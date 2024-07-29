create database mangxahoi
use mangxahoi

create Table role(
	id int NOT NULL PRIMARY KEY IDENTITY(1,1), 
	name VARCHAR(250) NOT NULL, 
);
CREATE TABLE users (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    email VARCHAR(250) NOT NULL,
    password VARCHAR(250) NOT NULL,
    fullname NVARCHAR(250),
	address NVARCHAR(250),
	age VARCHAR(250),
	phone VARCHAR(250),
	photo VARCHAR(250),
	roleid int,
	FOREIGN KEY (roleid) REFERENCES role(id),
);
CREATE TABLE friendships (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    userid1 int NOT NULL,
	status bit,
    userid2 int NOT NULL,
	date DateTime,
    FOREIGN KEY (userid1) REFERENCES users(id),
	FOREIGN KEY (userid2) REFERENCES users(id),
);
CREATE TABLE story (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    content NVARCHAR(250),
    userid int NOT NULL,
	status bit,
	photo VARCHAR(250),
	date DateTime,
    FOREIGN KEY (userid) REFERENCES users(id),
);
CREATE TABLE content (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    content NVARCHAR(250),
    date DateTime NOT NULL,
	status bit,
	userid int NOT NULL,
	contentPhoto VARCHAR(250),
    FOREIGN KEY (userid) REFERENCES users(id),
);
CREATE TABLE comments (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    comment NVARCHAR(250) ,
    userid int NOT NULL,
	date DateTime ,
    contentid int NOT NULL,
    FOREIGN KEY (userid) REFERENCES users(id),
    FOREIGN KEY (contentid) REFERENCES content(id)
);
CREATE TABLE likes (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    userid int NOT NULL	,
    contentid int NOT NULL,
	commentid int,
	date DateTime,
    FOREIGN KEY (userid) REFERENCES users(id),
	FOREIGN KEY (contentid) REFERENCES content(id),
	FOREIGN KEY (commentid) REFERENCES comments(id),
);
CREATE TABLE messages (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    useridgui int NOT NULL,
    useridnhan int NOT NULL,
	status bit,
	contentPhoto VARCHAR(250),
	date DateTime,
    FOREIGN KEY (useridgui) REFERENCES users(id),
	FOREIGN KEY (useridnhan) REFERENCES users(id),
);
CREATE TABLE chat (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    content NVARCHAR(250) ,
    userid int NOT NULL,
	date DateTime ,
    messagesid int NOT NULL,
    FOREIGN KEY (userid) REFERENCES users(id),
    FOREIGN KEY (messagesid) REFERENCES messages(id)
);
CREATE TABLE notification (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    commentid int NULL,
    likeid int NULL,
    friendshipid int NULL,
    status bit,
    contentid int NULL,
    messageid int NULL,
    storyid int NULL,
	userid int NOT NULL,
    FOREIGN KEY (commentid) REFERENCES comments(id),
    FOREIGN KEY (likeid) REFERENCES likes(id),
    FOREIGN KEY (contentid) REFERENCES content(id),
    FOREIGN KEY (friendshipid) REFERENCES friendships(id),
    FOREIGN KEY (messageid) REFERENCES messages(id),
    FOREIGN KEY (storyid) REFERENCES story(id),
	FOREIGN KEY (userid) REFERENCES users(id)
);
CREATE TABLE saves (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	 contentid int NOT NULL,
	  userid int NOT NULL,
	  date DateTime,
	  FOREIGN KEY (userid) REFERENCES users(id),
	  FOREIGN KEY (contentid) REFERENCES content(id),
);
CREATE TABLE report (
    id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	contentreport nvarchar(250),
	contentid int NOT NULL,
	userid int NOT NULL,
	date DateTime,
	FOREIGN KEY (userid) REFERENCES users(id),
	FOREIGN KEY (contentid) REFERENCES content(id),
);
insert into role (name) values
('Admin'),
('User')
INSERT INTO users (email, password, fullname, address, age, phone, photo,roleid) VALUES
('1@gmail.com', '$2a$10$F6OFS9E8MDKU/TB4w6ieruG67rH41cFYb3tJXvdcSB6lM4.jUC1Hu', N'Chỉ Nhược', 'Address One', '25', '1234567890', 'avatar4.jpg',2),
('2@gmail.com', '$2a$10$F6OFS9E8MDKU/TB4w6ieruG67rH41cFYb3tJXvdcSB6lM4.jUC1Hu', N'Như Yên', '123 Tân Phú', '25', '1234567890', 'avatar3.jpg',2),
('3@gmail.com', '$2a$10$F6OFS9E8MDKU/TB4w6ieruG67rH41cFYb3tJXvdcSB6lM4.jUC1Hu', N'Tấn Lộc', '123 Tân Bình', '25', '1234567890', 'avatar13.jpg',2);
INSERT INTO content (content, date, userid,status,contentPhoto) VALUES
(N'Dù biết chỉ là mơ mộng nhưng em vẫn mơ về a Lộc.', '2024-06-01 12:00:00', 1,1, 'avatar4.jpg'),
(N'Dù mọi trông gai có đến e vẫn đứng về phía a Lộc.','2024-07-01 12:00:00', 2,0, 'avatar9.jpg'),
(N'Trên thế giới có hàng triệu người đàn ông em chỉ yêu mình a Lộc.','2024-07-01 12:00:00', 2,0, 'avatar12.jpg')
INSERT INTO story (content, userid, status, photo, date)
VALUES
('music/ChungTaRoiSeHanhPhuc-JackJ97-12903446.mp3', 1, 1, 'avatar4.jpg', '2024-06-10 12:00:00'),
('music/DungLamTraiTimAnhDau.wav', 2, 1, 'avatar13.jpg', '2024-06-11 13:00:00');
INSERT INTO comments (comment, userid, date, contentid)
VALUES
('Comment 1', 2, '2024-06-02 14:00:00', 1),  -- User Two's comment on Content 1
('Comment 2', 1, '2024-06-03 15:00:00', 2),  -- User One's comment on Content 2
('Comment 3', 2, '2024-06-04 16:00:00', 3);  -- User Two's comment on Content 3
INSERT INTO likes (userid, contentid, date)
VALUES
(1, 2, '2024-06-03 16:00:00'),  -- User One likes Content 2
(2, 1, '2024-06-02 15:00:00'),  -- User Two likes Content 1
(1, 3, '2024-06-04 17:00:00');  -- User One likes Content 3
INSERT INTO report (contentreport, contentid,userid, date)
VALUES
(N'Có hành vi bạo lực',1, 1, '2024-06-03 16:00:00')
