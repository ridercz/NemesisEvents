-- Migrate Areas
PRINT 'Migrating areas...'
SET IDENTITY_INSERT NemesisEvents.dbo.Areas ON
INSERT
	INTO NemesisEvents.dbo.Areas (Id, Name)
	SELECT Id = AreaId, Name FROM Areas
SET IDENTITY_INSERT NemesisEvents.dbo.Areas OFF

-- Migrate Venues
PRINT 'Migrating venues...'
SET IDENTITY_INSERT NemesisEvents.dbo.Venues ON
INSERT
	INTO NemesisEvents.dbo.Venues (Id, AreaId, City, Description, Latitude, Longitude, Name, StreetAddress)
	SELECT Id = VenueId, AreaId, City = NULL, Description, Latitude, Longitude, Name, StreetAddress FROM Venues
SET IDENTITY_INSERT NemesisEvents.dbo.Venues OFF
UPDATE NemesisEvents.dbo.Venues SET City = LEFT(Name, CHARINDEX(' - ', Name) - 1) WHERE CHARINDEX(' - ', Name) > 0
UPDATE NemesisEvents.dbo.Venues SET City = LEFT(Name, CHARINDEX(' – ', Name) - 1) WHERE CHARINDEX(' – ', Name) > 0
UPDATE NemesisEvents.dbo.Venues SET Name = RIGHT(Name, LEN(Name) - LEN(City) - 3) WHERE City IS NOT NULL
UPDATE NemesisEvents.dbo.Venues SET City = Name WHERE City IS NULL AND AreaId > 1

-- Migrate Users
PRINT 'Migrating users...'
INSERT
	INTO NemesisEvents.dbo.AspNetUsers (
		AccessFailedCount,
		Email,
		EmailConfirmed,
		LockoutEnabled,
		NormalizedEmail,
		NormalizedUserName,
		PhoneNumberConfirmed,
		TwoFactorEnabled,
		UserName,
		FullName, 
		Enabled,
		DateCreated,
		DateLastLogin, 
		SecurityStamp)
	SELECT
		AccessFailedCount = 0,
		Email = U.Email,
		EmailConfirmed = 1,
		LockoutEnabled = 0,
		NormalizedEmail = UPPER(U.Email),
		NormalizedUserName = U.UserName,
		PhoneNumberConfirmed = 0,
		TwoFactorEnabled = 0,
		UserName,
		FullName = ISNULL(U.DisplayName, U.UserName),
		Enabled = U.IsApproved,
		U.DateCreated,
		DateLastLogin = ISNULL(U.DateLastLogin, U.DateCreated),
		SecurityStamp = NEWID()
	FROM Users AS U

-- Migrate email frequency
PRINT 'Updating user email frequency...'
UPDATE NemesisEvents.dbo.AspNetUsers SET EmailFrequency = 3 WHERE UserName IN (SELECT UserName FROM Users WHERE SendWeeklyMessages = 1)
UPDATE NemesisEvents.dbo.AspNetUsers SET EmailFrequency = 2 WHERE UserName IN (SELECT UserName FROM Users WHERE SendDailyMessages = 1)
UPDATE NemesisEvents.dbo.AspNetUsers SET EmailFrequency = 1 WHERE UserName IN (SELECT UserName FROM Users WHERE SendSingleMessages = 1)

-- Migrate roles
PRINT 'Creating roles if needed...'
DECLARE @AdmRoleId int
SELECT @AdmRoleId = Id FROM NemesisEvents.dbo.AspNetRoles WHERE Name = 'Administrators'
IF (@AdmRoleId IS NULL) BEGIN
	INSERT INTO NemesisEvents.dbo.AspNetRoles VALUES (NEWID(), 'Administrators', 'ADMINISTRATORS')
	SELECT @AdmRoleId = SCOPE_IDENTITY()
END
DECLARE @OrgRoleId int
SELECT @OrgRoleId = Id FROM NemesisEvents.dbo.AspNetRoles WHERE Name = 'Organizers'
IF (@OrgRoleId IS NULL) BEGIN
	INSERT INTO NemesisEvents.dbo.AspNetRoles VALUES (NEWID(), 'Organizers', 'ORGANIZERS')
	SELECT @OrgRoleId = SCOPE_IDENTITY()
END
PRINT 'Updating user roles...'
INSERT INTO NemesisEvents.dbo.AspNetUserRoles SELECT UserId = Id, RoleId = @AdmRoleId FROM NemesisEvents.dbo.AspNetUsers WHERE UserName IN (SELECT UserName FROM Users WHERE IsAdministrator = 1)
INSERT INTO NemesisEvents.dbo.AspNetUserRoles SELECT UserId = Id, RoleId = @OrgRoleId FROM NemesisEvents.dbo.AspNetUsers WHERE UserName IN (SELECT UserName FROM Users WHERE IsOrganizer = 1)

-- Migrate Events
PRINT 'Migrating events...'
SET IDENTITY_INSERT NemesisEvents.dbo.Events ON
INSERT
	INTO NemesisEvents.dbo.Events (
		Id,
		AdmissionFee,
		AllowRegistration, DateBegin, DateEnd, DateCreated, Description, InvitationSent, Name,
		OrganizerName,
		OwnerId,
		UseRegistration, VenueId
	)
	SELECT
		Id = E.EventId,
		AdmissionFee = LTRIM(STR(AdmissionFee) + ' Kè'),
		AllowRegistration, DateBegin, DateEnd, E.DateCreated, Description, InvitationSent, Name,
		OrganizerName = Organizer,
		OwnerId = U.Id,
		UseRegistration, VenueId
	FROM Events AS E
	LEFT JOIN NemesisEvents.dbo.AspNetUsers AS U ON E.OwnerUserName = U.UserName
SET IDENTITY_INSERT NemesisEvents.dbo.Events OFF
PRINT 'Normalizing free event prices...'
UPDATE NemesisEvents.dbo.Events SET AdmissionFee = NULL WHERE AdmissionFee = '0 Kè'

-- Migrate Attendees
PRINT 'Migrating attendees...'
INSERT
	INTO NemesisEvents.dbo.Attendees (DateRegistered, EventId, UserId)
	SELECT
		DateRegistered = GETDATE(),
		EventId =  A.Event_EventId,
		UserId = U.Id
	FROM UserEvents AS A
	LEFT JOIN NemesisEvents.dbo.AspNetUsers AS U ON A.User_UserName = U.UserName

-- Migrate UserAreas
PRINT 'Migrating user watched areas...'
INSERT
	INTO NemesisEvents.dbo.UserAreas(AreaId, UserId)
	SELECT
		AreaId =  A.Area_AreaId,
		UserId = U.Id
	FROM UserAreas AS A
	LEFT JOIN NemesisEvents.dbo.AspNetUsers AS U ON A.User_UserName = U.UserName

-- Normalize users
PRINT 'Finding duplicate users...'
SELECT U.Id, U.UserName, U.Email, A.Count
FROM NemesisEvents.dbo.AspNetUsers AS U
LEFT JOIN (SELECT UserId, COUNT(*) AS Count FROM NemesisEvents.dbo.Attendees GROUP BY UserId) AS A ON U.Id = A.UserId
WHERE Email IN (SELECT Email FROM NemesisEvents.dbo.AspNetUsers GROUP BY Email HAVING COUNT(*) > 1)
ORDER BY U.Email, Id

PRINT 'Consolidating duplicate users...'
UPDATE NemesisEvents.dbo.Attendees SET UserId = 83   WHERE UserId = 120
UPDATE NemesisEvents.dbo.Attendees SET UserId = 2829 WHERE UserId = 3663
UPDATE NemesisEvents.dbo.Attendees SET UserId = 253  WHERE UserId = 3149
UPDATE NemesisEvents.dbo.Attendees SET UserId = 2825 WHERE UserId = 3604
UPDATE NemesisEvents.dbo.Attendees SET UserId = 3523 WHERE UserId = 3526
UPDATE NemesisEvents.dbo.Attendees SET UserId = 1458 WHERE UserId = 4066
UPDATE NemesisEvents.dbo.Attendees SET UserId = 4287 WHERE UserId = 4660
UPDATE NemesisEvents.dbo.Attendees SET UserId = 4409 WHERE UserId = 4478

UPDATE NemesisEvents.dbo.UserAreas SET UserId = 83   WHERE UserId = 120
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 2829 WHERE UserId = 3663
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 253  WHERE UserId = 3149
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 2825 WHERE UserId = 3604
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 3523 WHERE UserId = 3526
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 1458 WHERE UserId = 4066
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 4287 WHERE UserId = 4660
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 4409 WHERE UserId = 4478

PRINT 'Deleting duplicate users...'
DELETE FROM NemesisEvents.dbo.AspNetUsers WHERE Id IN (120, 3663, 3149, 3604, 3526, 4066, 4660, 4478)

PRINT 'Finding duplicate users again...'
SELECT U.Id, U.UserName, U.Email
FROM NemesisEvents.dbo.AspNetUsers AS U
WHERE Email IN (SELECT Email FROM NemesisEvents.dbo.AspNetUsers GROUP BY Email HAVING COUNT(*) > 1)
ORDER BY U.Email, U.Id

PRINT 'Changing user names to email addresses...'
UPDATE NemesisEvents.dbo.AspNetUsers SET UserName = Email, NormalizedUserName = UPPER(Email)

-- Add tags
PRINT 'Creating tags...'
IF NOT EXISTS (SELECT * FROM NemesisEvents.dbo.Tags WHERE Name = 'Vývojáøi')  INSERT INTO NemesisEvents.dbo.Tags VALUES ('Vývojáøi')
IF NOT EXISTS (SELECT * FROM NemesisEvents.dbo.Tags WHERE Name = 'IT profesionálové')  INSERT INTO NemesisEvents.dbo.Tags VALUES ('IT profesionálové')
IF NOT EXISTS (SELECT * FROM NemesisEvents.dbo.Tags WHERE Name = 'Uživatelé')  INSERT INTO NemesisEvents.dbo.Tags VALUES ('Uživatelé')

-- Add all tags to all users
PRINT 'Adding all tags to all users...'
INSERT
	INTO NemesisEvents.dbo.UserTags (UserId, TagId)
	SELECT
		UserId = U.Id,
		TagId = T.Id
	FROM NemesisEvents.dbo.AspNetUsers AS U
	CROSS JOIN NemesisEvents.dbo.Tags AS T
	ORDER BY 1