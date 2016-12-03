-- Migrate Areas
SET IDENTITY_INSERT NemesisEvents.dbo.Areas ON
INSERT
	INTO NemesisEvents.dbo.Areas (Id, Name)
	SELECT Id = AreaId, Name FROM Areas
SET IDENTITY_INSERT NemesisEvents.dbo.Areas OFF

-- Migrate Venues
SET IDENTITY_INSERT NemesisEvents.dbo.Venues ON
INSERT
	INTO NemesisEvents.dbo.Venues (Id, AreaId, City, Description, Latitude, Longitude, Name, StreetAddress)
	SELECT Id = VenueId, AreaId, City = NULL, Description, Latitude, Longitude, Name, StreetAddress FROM Venues
SET IDENTITY_INSERT NemesisEvents.dbo.Venues OFF
UPDATE NemesisEvents.dbo.Venues SET City = LEFT(Name, CHARINDEX(' - ', Name) - 1) WHERE CHARINDEX(' - ', Name) > 0
UPDATE NemesisEvents.dbo.Venues SET City = LEFT(Name, CHARINDEX(' � ', Name) - 1) WHERE CHARINDEX(' � ', Name) > 0
UPDATE NemesisEvents.dbo.Venues SET Name = RIGHT(Name, LEN(Name) - LEN(City) - 3) WHERE City IS NOT NULL
UPDATE NemesisEvents.dbo.Venues SET City = Name WHERE City IS NULL AND AreaId > 1

-- Migrate Users
INSERT
	INTO NemesisEvents.dbo.AspNetUsers (
		AccessFailedCount,
		Email,
		EmailConfirmed,
		LockoutEnabled,
		NormalizedEmail,
		NormalizedUserName,
		PhoneNumberConfirmed,
		SendDailyMessages,
		SendSingleMessages,
		SendWeeklyMessages,
		TwoFactorEnabled,
		UserName)
	SELECT
		AccessFailedCount = 0,
		Email = U.Email,
		EmailConfirmed = 1,
		LockoutEnabled = 0,
		NormalizedEmail = U.Email,
		NormalizedUserName = U.UserName,
		PhoneNumberConfirmed = 0,
		U.SendDailyMessages,
		U.SendSingleMessages,
		U.SendWeeklyMessages,
		TwoFactorEnabled = 0,
		UserName
	FROM Users AS U

-- Migrate roles
DECLARE @AdmRoleId int
SELECT @AdmRoleId = Id FROM NemesisEvents.dbo.AspNetRoles WHERE Name = 'Administrators'
IF (@AdmRoleId IS NULL) BEGIN
	INSERT INTO NemesisEvents.dbo.AspNetRoles VALUES (NEWID(), 'Administrators', 'administrators')
	SELECT @AdmRoleId = SCOPE_IDENTITY()
END
DECLARE @OrgRoleId int
SELECT @OrgRoleId = Id FROM NemesisEvents.dbo.AspNetRoles WHERE Name = 'Organizers'
IF (@OrgRoleId IS NULL) BEGIN
	INSERT INTO NemesisEvents.dbo.AspNetRoles VALUES (NEWID(), 'Organizers', 'organizers')
	SELECT @OrgRoleId = SCOPE_IDENTITY()
END
INSERT INTO NemesisEvents.dbo.AspNetUserRoles SELECT UserId = Id, RoleId = @AdmRoleId FROM NemesisEvents.dbo.AspNetUsers WHERE UserName IN (SELECT UserName FROM Users WHERE IsAdministrator = 1)
INSERT INTO NemesisEvents.dbo.AspNetUserRoles SELECT UserId = Id, RoleId = @OrgRoleId FROM NemesisEvents.dbo.AspNetUsers WHERE UserName IN (SELECT UserName FROM Users WHERE IsOrganizer = 1)

-- Migrate Events
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
		AdmissionFee = LTRIM(STR(AdmissionFee) + ' K�'),
		AllowRegistration, DateBegin, DateEnd, DateCreated, Description, InvitationSent, Name,
		OrganizerName = Organizer,
		OwnerId = U.Id,
		UseRegistration, VenueId
	FROM Events AS E
	LEFT JOIN NemesisEvents.dbo.AspNetUsers AS U ON E.OwnerUserName = U.UserName
SET IDENTITY_INSERT NemesisEvents.dbo.Events OFF

-- Migrate Attendees
INSERT
	INTO NemesisEvents.dbo.Attendees (DateRegistered, EventId, UserId)
	SELECT
		DateRegistered = GETDATE(),
		EventId =  A.Event_EventId,
		UserId = U.Id
	FROM UserEvents AS A
	LEFT JOIN NemesisEvents.dbo.AspNetUsers AS U ON A.User_UserName = U.UserName

-- Migrate UserAreas
INSERT
	INTO NemesisEvents.dbo.UserAreas(AreaId, UserId)
	SELECT
		AreaId =  A.Area_AreaId,
		UserId = U.Id
	FROM UserAreas AS A
	LEFT JOIN NemesisEvents.dbo.AspNetUsers AS U ON A.User_UserName = U.UserName

-- Normalize users
SELECT U.Id, U.UserName, U.Email, A.Count
FROM NemesisEvents.dbo.AspNetUsers AS U
LEFT JOIN (SELECT UserId, COUNT(*) AS Count FROM NemesisEvents.dbo.Attendees GROUP BY UserId) AS A ON U.Id = A.UserId
WHERE Email IN (SELECT Email FROM NemesisEvents.dbo.AspNetUsers GROUP BY Email HAVING COUNT(*) > 1)
ORDER BY U.Email, Id

UPDATE NemesisEvents.dbo.Attendees SET UserId = 83   WHERE UserId = 120
UPDATE NemesisEvents.dbo.Attendees SET UserId = 2829 WHERE UserId = 3663
UPDATE NemesisEvents.dbo.Attendees SET UserId = 253  WHERE UserId = 3149
UPDATE NemesisEvents.dbo.Attendees SET UserId = 2825 WHERE UserId = 3604
UPDATE NemesisEvents.dbo.Attendees SET UserId = 3523 WHERE UserId = 3526
UPDATE NemesisEvents.dbo.Attendees SET UserId = 1458 WHERE UserId = 4065
UPDATE NemesisEvents.dbo.Attendees SET UserId = 4286 WHERE UserId = 4659
UPDATE NemesisEvents.dbo.Attendees SET UserId = 4477 WHERE UserId = 4408

UPDATE NemesisEvents.dbo.UserAreas SET UserId = 83   WHERE UserId = 120
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 2829 WHERE UserId = 3663
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 253  WHERE UserId = 3149
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 2825 WHERE UserId = 3604
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 3523 WHERE UserId = 3526
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 1458 WHERE UserId = 4065
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 4286 WHERE UserId = 4659
UPDATE NemesisEvents.dbo.UserAreas SET UserId = 4477 WHERE UserId = 4408

DELETE FROM NemesisEvents.dbo.AspNetUsers WHERE Id IN (120, 3663, 3149, 3604, 3526, 4065, 4659, 4408)

-- Add tags
IF NOT EXISTS (SELECT * FROM NemesisEvents.dbo.Tags WHERE Name = 'V�voj��i')  INSERT INTO NemesisEvents.dbo.Tags VALUES ('V�voj��i')
IF NOT EXISTS (SELECT * FROM NemesisEvents.dbo.Tags WHERE Name = 'IT profesion�lov�')  INSERT INTO NemesisEvents.dbo.Tags VALUES ('IT profesion�lov�')
IF NOT EXISTS (SELECT * FROM NemesisEvents.dbo.Tags WHERE Name = 'U�ivatel�')  INSERT INTO NemesisEvents.dbo.Tags VALUES ('U�ivatel�')

-- Add all tags to all users
INSERT
	INTO NemesisEvents.dbo.UserTags (UserId, TagId)
	SELECT
		UserId = U.Id,
		TagId = T.Id
	FROM NemesisEvents.dbo.AspNetUsers AS U
	CROSS JOIN NemesisEvents.dbo.Tags AS T
	ORDER BY 1