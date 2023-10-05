GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Server_SQL')
BEGIN
    CREATE DATABASE Server_SQL;
END;

GO
use Server_SQL

GO

-- TABLE CREATION
CREATE TABLE Roles(
		-- 0 = guest, 1 = user, 2 = admin
		role_ID TINYINT PRIMARY KEY,
		role_name VARCHAR(16)
	)

CREATE TABLE LoginStatus (
	-- 0 = blocked, 1 = success, 2 = fail, 3 = waiting for release, 4 = Registered
	Status_ID TINYINT PRIMARY KEY,
	Status_name VARCHAR(16)
)

CREATE TABLE RestoreQuestions(
	Q_ID TINYINT IDENTITY(1,1) PRIMARY KEY,
	Question NVARCHAR(256) not null
)

CREATE TABLE Users(
	email VARCHAR(256) PRIMARY KEY,
	psw NVARCHAR(256) not null, -- add check for length and characters?
	first_name VARCHAR(20) not null,
	last_name VARCHAR(20) not null,
	dob date, -- add check for date 'smaller' than today
	user_role TINYINT not null,
	-- constraints
	CHECK (email LIKE '_%@_%._%'),
	CHECK (DATEDIFF(YEAR, dob, GETDATE()) > 1),
	-- foreign keys
	FOREIGN KEY (user_role) REFERENCES Roles (role_ID) ON DELETE CASCADE ON UPDATE CASCADE
	)

CREATE TABLE PasswordRestore(
	email VARCHAR(256),
	Q_ID TINYINT,
	answer VARCHAR(256) not null,
	--constraints
	FOREIGN KEY (email) REFERENCES Users (email) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (Q_ID) REFERENCES RestoreQuestions (Q_ID) ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY (email, Q_ID)
)

CREATE TABLE UserLog(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	email VARCHAR(256),
	log_time datetime not null,
	passed bit not null,
	--constraints
	FOREIGN KEY (email) REFERENCES Users (email) ON DELETE CASCADE ON UPDATE CASCADE,
)

CREATE TABLE LoginAttemptsLog (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	email VARCHAR(256),
	log_time datetime not null,
	Attempt_status TINYINT,
	--constraints
	FOREIGN KEY (email) REFERENCES Users (email) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (Attempt_status) REFERENCES LoginStatus (Status_ID) ON DELETE CASCADE ON UPDATE CASCADE,
)

GO
-- TRIGGERS
-- used when user registers, it inserts a successful attempt log to negate null cases
CREATE OR ALTER TRIGGER RegisterTrg
ON Users
AFTER INSERT
AS
	declare @email VARCHAR(256)
begin
	select *
	into #NewUserstable
	from inserted

	while((SELECT COUNT(*) FROM #NewUserstable) > 0)
	begin
		SELECT TOP 1 @email = email
		FROM #NewUserstable
		INSERT INTO LoginAttemptsLog (email, log_time, Attempt_status) values (@email, GETDATE(), 4)
		DELETE #NewUserstable WHERE email = @email
	end

	drop table #NewUserstable

end

GO
-- adds a log to the UserLog table based on entry from LoginAttemptsLog table
CREATE OR ALTER TRIGGER LoginAttemptTrg
ON LoginAttemptsLog
AFTER INSERT
AS
	declare @email VARCHAR(256)
	declare @loginResult tinyint
	declare @loginStatus bit
	declare @id int

begin
	select *
	into #logintable
	from inserted

	while((SELECT COUNT(*) FROM #logintable) > 0)
	begin
		SELECT TOP 1 @id = ID, @email = email, @loginResult = Attempt_status
		FROM #logintable

		if @loginResult = 1 OR @loginResult = 4
		begin
			set @loginStatus = 'TRUE'
		end
		else
		begin
			set @loginStatus = 'FALSE'
		end

		INSERT INTO UserLog (email, log_time, passed) VALUES (@email, GETDATE(), @loginStatus)
		DELETE #logintable WHERE ID = @id
	end

	DROP table #logintable
end

GO
-- DATA INSERTS
--ROLES
insert into Roles (role_ID, role_name) values (0, 'Guest');
insert into Roles (role_ID, role_name) values (1, 'User');
insert into Roles (role_ID, role_name) values (2, 'Admin');
-- LOGIN STATUS
insert into LoginStatus (Status_ID, Status_name) values (0, 'Blocked');
insert into LoginStatus (Status_ID, Status_name) values (1, 'Success');
insert into LoginStatus (Status_ID, Status_name) values (2, 'Fail');
insert into LoginStatus (Status_ID, Status_name) values (3, 'awaiting release');
insert into LoginStatus (Status_ID, Status_name) values (4, 'Registered');
--RESTORE QUESTIONS
insert into RestoreQuestions (Question) values ('What is your mother`s maiden name?');
insert into RestoreQuestions (Question) values ('What was the name of your first pet?');
insert into RestoreQuestions (Question) values ('In which city were you born?');
insert into RestoreQuestions (Question) values ('What is your favorite book?');
insert into RestoreQuestions (Question) values ('What is the name of your childhood best friend?');
insert into RestoreQuestions (Question) values ('What was the make and model of your first car?');
insert into RestoreQuestions (Question) values ('What is the name of your favorite teacher?');
insert into RestoreQuestions (Question) values ('What is your favorite movie?');
insert into RestoreQuestions (Question) values ('What was the first concert you attended?');
insert into RestoreQuestions (Question) values ('What is the name of your favorite fictional character?');
insert into RestoreQuestions (Question) values ('What is your favorite sports team?');
insert into RestoreQuestions (Question) values ('What is the street where you grew up?');
insert into RestoreQuestions (Question) values ('What is the name of your first school?');
insert into RestoreQuestions (Question) values ('What is your favorite food?');
insert into RestoreQuestions (Question) values ('What is your favorite color?');
insert into RestoreQuestions (Question) values ('What is your father`s middle name?');
insert into RestoreQuestions (Question) values ('What is your favorite vacation spot?');
insert into RestoreQuestions (Question) values ('What is the name of your first employer?');
insert into RestoreQuestions (Question) values ('What is your favorite hobby?');
insert into RestoreQuestions (Question) values ('What is your favorite musician or band?');
insert into RestoreQuestions (Question) values ('What is your favorite song?');
insert into RestoreQuestions (Question) values ('What is your favorite historical figure?');
insert into RestoreQuestions (Question) values ('What is your favorite season?');
insert into RestoreQuestions (Question) values ('What is the name of the first company you worked for?');
insert into RestoreQuestions (Question) values ('What is the name of your favorite childhood toy?');
insert into RestoreQuestions (Question) values ('What is your favorite type of cuisine?');
insert into RestoreQuestions (Question) values ('What is your favorite landmark or place?');
insert into RestoreQuestions (Question) values ('What is the name of your first cousin?');
insert into RestoreQuestions (Question) values ('What is the name of the street where your grandparents lived?');
insert into RestoreQuestions (Question) values ('What is your favorite type of animal?');


--USERS
insert into users (email, psw, first_name, last_name, dob, user_role)
values ('ariel.vaintraub@gmail.com', '1234', 'Ori' , 'Tsemach', '1994-10-10', 2);

insert into users (email, psw, first_name, last_name, dob, user_role)
values ('ori.tsemach@gmail.com', '12345', 'Ariel' , 'Vaintraub', '1997-07-24', 2);

insert into users (email, psw, first_name, last_name, dob, user_role)
values ('tezbo@icloud.com', 'qwer', 'Tezbo' , 'Icloud', '1993-08-12', 0);

insert into users (email, psw, first_name, last_name, dob, user_role)
values ('drezet@live.com', 'asdf', 'Drezet' , 'Live', '1987-11-23', 1);

insert into users (email, psw, first_name, last_name, dob, user_role)
values ('maneesh@me.com', 'zxcv', 'Maneesh' , 'Maneesh', '1983-04-16', 1);

GO
-- views
CREATE OR ALTER VIEW CurrentUserStatus AS
	SELECT email, log_time, Attempt_status
	FROM LoginAttemptsLog
	Where ID in (
		SELECT MAX(ID)
		FROM LoginAttemptsLog
		GROUP BY email)
		
	

GO
CREATE OR ALTER VIEW LastSuccessfulUserStatus AS
	SELECT email, log_time
	FROM LoginAttemptsLog
	Where ID in (
		SELECT MAX(ID)
		FROM LoginAttemptsLog
		WHERE Attempt_status = 1
		GROUP BY email)
	

GO
CREATE OR ALTER VIEW BlockedUsers AS
	SELECT email, log_time
	FROM LoginAttemptsLog
	Where ID in (
		SELECT MAX(ID)
		FROM LoginAttemptsLog
		WHERE Attempt_status = 0 and DATEDIFF(MINUTE,log_time, GETDATE()) < 20
		GROUP BY email)
	

GO
CREATE OR ALTER VIEW UserRole AS
	SELECT email, user_role
	FROM Users

GO
-- PROCEDURES
-- checks login attempts with a set of conditions
CREATE OR ALTER PROCEDURE UserLoginProc @email VARCHAR(256), @psw NVARCHAR(256)
as
declare @valid_user bit = (SELECT COUNT(*) -- check if username matches password
						   FROM Users
						   WHERE email = @email and psw =@psw)

declare @attempts_count int = (SELECT COUNT(*) -- check how many login attempts were made in the last 3 minutes
							   FROM UserLog
							   WHERE email = @email and passed = 0 and DATEDIFF(MINUTE, log_time, GETDATE()) <= 3)

declare @current_status int = (SELECT Attempt_status
								  	     FROM CurrentUserStatus
										 WHERE email = @email)


declare	@current_status_time datetime = (SELECT log_time
								  	     FROM CurrentUserStatus
										 WHERE email = @email)

if @current_status = 3 -- meaning user is blocked but attempted a correct login before 20 minutes elapsed
begin
	set @current_status = 0
	set @current_status_time = (SELECT log_time
								FROM BlockedUsers
								WHERE email = @email)
end

-- username and passwords check, under 3 attempts and not blocked
if @valid_user = 1 and @attempts_count <= 3 and @current_status != 0 
begin
	INSERT INTO LoginAttemptsLog (email, log_time, Attempt_status) values (@email, GETDATE(), 1) --succesful login
end
--username and passwords fails, under 3 attempts and not blocked
else if @valid_user = 0 and @attempts_count <= 3 and @current_status != 0 
begin
	INSERT INTO LoginAttemptsLog (email, log_time, Attempt_status) values (@email, GETDATE(), 2) --failed login
end
--username and passwords fails, over 3 attempts and not blocked
else if @valid_user = 0 and @attempts_count > 3 and @current_status != 0 
begin
	INSERT INTO LoginAttemptsLog (email, log_time, Attempt_status) values (@email, GETDATE(), 0) --user blocked
end
--username and passwords checks and blocked
else if @valid_user = 1 and @current_status = 0 
begin
	if DATEDIFF(MINUTE, @current_status_time, GETDATE()) > 20 -- for over 20 minutes
	begin
		INSERT INTO LoginAttemptsLog (email, log_time, Attempt_status) values (@email, GETDATE(), 1)
	end
	else -- under or at 20 minutes
	begin
		INSERT INTO LoginAttemptsLog (email, log_time, Attempt_status) values (@email, GETDATE(), 3)
	end
end
--username and passwords fails and blocked
else if @valid_user = 0 and @current_status = 0 
begin
	INSERT INTO LoginAttemptsLog (email, log_time, Attempt_status) values (@email, GETDATE(), 0)
end
GO

-- changed version for HW 3
-- changed the return value of the procedure to be amount of rows of requested query
CREATE OR ALTER PROCEDURE LogInStatusProc @email VARCHAR(256)
as
	declare @returnValue int = (SELECT count(*)
								FROM LastSuccessfulUserStatus
								WHERE email = @email and DATEDIFF(SECOND, log_time, GETDATE()) <= 30)
	declare @failed int = (SELECT Attempt_status
							FROM CurrentUserStatus
							WHERE email = @email)
begin
	if (@failed != 1)
	begin
		RETURN 0
	end
	RETURN @returnValue
end
GO


--get list of all blocked users within the last 20 minutes
CREATE OR ALTER PROCEDURE GetBlockedUsersProc @email VARCHAR(256)
as
declare @role tinyint = (SELECT user_role
						 FROM Users
						 WHERE email = @email)
if @role = 2
begin
	SELECT email
	FROM BlockedUsers
end
else
begin
	print 'Access Denied'
end

GO
--get list of the last successful attempts of all users
CREATE OR ALTER PROCEDURE GetSuccessfulUsersProc @email VARCHAR(256)
as
declare @role tinyint = (SELECT user_role
						 FROM Users
						 WHERE email = @email)
if @role = 2
begin
	SELECT email, CAST(log_time as smalldatetime) as 'Last Login Time'
	FROM LastSuccessfulUserStatus
end
else
begin
	print 'Access Denied'
end

GO
-- get history of own login attempts
CREATE OR ALTER PROCEDURE GetLoginHistoryProc @email VARCHAR(256)
as
declare @role tinyint = (SELECT user_role
						 FROM Users
						 WHERE email = @email)
if @role != 0
begin
	SELECT log_time as "Log Time", Status_name as "Login Status"
	FROM LoginAttemptsLog as LA inner join LoginStatus as LS on LA.Attempt_status = LS.Status_ID
	WHERE email = @email
end
else
begin
	print 'Access Denied'
end

GO
--/*
-- execute procedure
-- WARNING -> DO NOT EXECUTE ALL AT THE SAME TIME, THIS CAUSES BUGS
-- UserLoginProc
exec UserLoginProc @email = 'ariel.vaintraub@gmail.com', @psw = '1234' -- correct entry
exec UserLoginProc @email = 'ariel.vaintraub@gmail.com', @psw = '4321' -- false entry

exec UserLoginProc @email = 'tezbo@icloud.com', @psw = '1111' -- false entry
exec UserLoginProc @email = 'tezbo@icloud.com', @psw = '2222' -- false entry
exec UserLoginProc @email = 'tezbo@icloud.com', @psw = '3333'  -- false entry and blocked
exec UserLoginProc @email = 'tezbo@icloud.com', @psw = '4444' -- new blocked entry
exec UserLoginProc @email = 'tezbo@icloud.com', @psw = '5555' -- awaiting release entry

-- LogInStatusProc - in this 
DECLARE @test int
exec @test = LogInStatusProc @email = 'ariel.vaintraub@gmail.com' -- check status - should be true for testing purpose
SELECT 'return value' = @test

-- GetBlockedUsersProc
exec GetBlockedUsersProc @email = 'ariel.vaintraub@gmail.com' -- check status - should be true for testing purpose

-- GetSuccessfulUsersProc
exec GetSuccessfulUsersProc @email = 'ariel.vaintraub@gmail.com' -- check status - should be true for testing purpose

-- GetLoginHistoryProc
exec GetLoginHistoryProc @email = 'ariel.vaintraub@gmail.com' -- check status - should be true for testing purpose
--*/
GO
-- HW3
-- procedures
--changes to user table
CREATE OR ALTER PROCEDURE UsersTableChangeProc
	(@email VARCHAR(256) = '',
	 @psw NVARCHAR(256) = '',
	 @f_name VARCHAR(20) = '',
	 @l_name VARCHAR(20) = '',
	 @dob date = NULL,
	 @actionType NVARCHAR(20) = '')
AS
BEGIN
	IF @actionType = 'Insert'
	BEGIN
		INSERT INTO Users (email, psw, first_name, last_name, dob, user_role)
		VALUES			  (@email, @psw, @f_name, @l_name, @dob, 1)
	END
	ELSE IF @actionType = 'Update'
	BEGIN
		UPDATE Users
		SET	first_name = @f_name,
			last_name = @l_name,
			dob = @dob
		WHERE email = @email
	END
	ELSE IF @actionType = 'Delete'
	BEGIN
		DELETE FROM Users
		WHERE email = @email
	END
	ELSE IF @actionType = 'NewPass'
		UPDATE Users
		SET psw = @psw
		WHERE email = @email
END

GO

--insert to PasswordRestore table <-> link questions to user
CREATE OR ALTER PROCEDURE PasswordRestoreTableInsertProc
	(@email VARCHAR(256),
	 @qid TINYINT,
	 @ans VARCHAR(256))
AS
BEGIN
	INSERT INTO PasswordRestore (email, Q_ID, answer)
	VALUES						(@email, @qid, @ans)
END

GO

--get user questions and answers
CREATE OR ALTER PROCEDURE QNAProc @email varchar(256)
AS
DECLARE @isUserExist INT = (SELECT COUNT(*)
							FROM Users
							WHERE email = @email)
BEGIN
	IF @isUserExist = 0
	BEGIN
		SELECT Question
		FROM RestoreQuestions
	END
	ELSE
	BEGIN
		SELECT Question, PR.Q_ID, answer
		FROM PasswordRestore as PR inner join RestoreQuestions as RQ on PR.Q_ID = RQ.Q_ID
		WHERE email = @email
	END
	
END
GO

CREATE OR ALTER PROCEDURE CurrentUserStatusProc
AS
BEGIN
	SELECT *
	FROM CurrentUserStatus
END
GO

CREATE OR ALTER PROCEDURE UserRoleProc @email varchar(256)
AS
BEGIN
	SELECT *
	FROM UserRole
	Where email = @email
END
GO


CREATE OR ALTER PROCEDURE GetQuestionsProc
AS
BEGIN
	SELECT *
	FROM RestoreQuestions
END
GO

CREATE OR ALTER PROCEDURE UserExistsProc @email varchar(256)
AS
BEGIN
	SELECT email
	FROM Users
	WHERE email = @email
END
GO


--HW4
--DROP LOGIN DBAdmin
--DROP USER The_Admin
--DROP LOGIN DBUser
--DROP USER The_User
--DROP LOGIN DBGuest
--DROP USER The_Guest
-- admin user
CREATE LOGIN DBAdmin WITH PASSWORD = '12cd';
USE Server_SQL
GO
CREATE USER The_Admin FOR LOGIN DBAdmin
GO
-- regular user
CREATE LOGIN DBUser WITH PASSWORD = '1234';
USE Server_SQL
GO
CREATE USER The_User FOR LOGIN DBUser
GO
-- guest user
CREATE LOGIN DBGuest WITH PASSWORD = 'abcd';
USE Server_SQL
GO
CREATE USER The_Guest FOR LOGIN DBGuest
GO

-- access
USE Server_SQL
GRANT EXEC ON GetBlockedUsersProc TO The_Admin
GRANT EXEC ON CurrentUserStatusProc TO The_Admin
GRANT EXEC ON GetLoginHistoryProc TO The_Admin
GRANT EXEC ON GetSuccessfulUsersProc TO The_Admin
GRANT EXEC ON LogInStatusProc TO The_Admin


GRANT EXEC ON GetLoginHistoryProc TO The_User
GRANT EXEC ON PasswordRestoreTableInsertProc TO The_User
GRANT EXEC ON UsersTableChangeProc TO The_User
GRANT EXEC ON UserRoleProc TO The_User

GRANT EXEC ON UserLoginProc TO The_Guest
GRANT EXEC ON QNAProc TO The_Guest
GRANT INSERT ON Users TO The_Guest
GRANT EXEC ON LogInStatusProc TO The_Guest
GRANT EXEC ON GetQuestionsProc TO The_Guest
GRANT EXEC ON UserExistsProc TO The_Guest
GRANT EXEC ON UsersTableChangeProc TO The_Guest
GRANT EXEC ON PasswordRestoreTableInsertProc TO The_Guest
