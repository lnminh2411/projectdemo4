

create table [Category](
    CategoryId int primary key identity(1,1),
    CategoryName nvarchar(50) not null
)
go

go
create table [Seller](
    SellerId int primary key identity(1,1),
    Name nvarchar(50) not null,
    Gender bit not null,
    Email varchar(50),
    Birthdate date not null,
    Address nvarchar(100),
    Phone varchar(10),
    isActivate bit not null,
    Password varchar(50) not null,
	UserId int 
)
go
create table [Agent](
    AgentId int primary key identity(1,1),
    AgentName nvarchar(50) not null,
    Email varchar(50),
    Address nvarchar(100),
    Phone varchar(10),
    isActivate bit not null,
    Password varchar(50) not null,
    Introduction nvarchar(max),
    EmailHide bit not null,
    paymentId int,
	UserId int
)
go


create table [Advertisement](
    adsId int primary key identity(1,1),
    Tiltle nvarchar(100),        
    ReleaseDate date not null,
    ExpirationDate date not null,
    SellerId int,
    AgentId int,    
    PaymentId int, 	
    CategoryId int,
	Describe nvarchar(max),    
    CurrentSymbol varchar(10),
    priceOfAds float,
    EstatePrice float not null,
    Facade float,
    Gateway float,
    floors int,
    Bedrooms int,
    Toilets int,
    furniture nvarchar(250),
    Area float not null,
	Cityprovince nvarchar(50),
	District nvarchar(50),
    Ward nvarchar(50),
    Street nvarchar(50),
	isActivate bit not null,
	UserId int ,
	StatusHouse nvarchar(20) not null
)
go

create table [User](
    UserId int primary key identity(1,1),
    UserName varchar(50) not null,
    Password varchar(50) not null
)
go 
create table [Image](
    ImageId int primary key identity(1,1),
    FileName nvarchar(50) not null,
    Extension nvarchar(50) not null,
    AdsId int,
)
go
create table [Report](
    ReportId int primary key identity(1,1),
    ReportDate date,
    AdsId int,
    SellerId int,
    AgentId int,
    Price float
)
go 
create table [Payment](
    PaymentId int primary key identity(1,1),
    PaymentName nvarchar(50)
)


go
ALTER TABLE [Advertisement] 
ADD CONSTRAINT FK_Agent_Adsvertise FOREIGN KEY (AgentId) REFERENCES [Agent](AgentId);

go
ALTER TABLE [Advertisement] 
ADD CONSTRAINT FK_Seller_Adsvertise FOREIGN KEY (SellerId) REFERENCES [Seller](SellerId);

go
ALTER TABLE [Advertisement]
ADD CONSTRAINT FK_Payment_Adsvertise FOREIGN KEY (PaymentId) REFERENCES [Payment](PaymentId);

go
ALTER TABLE [Advertisement] 
ADD CONSTRAINT FK_User_Adsvertise FOREIGN KEY (UserId) REFERENCES [User](UserId);

go
ALTER TABLE [Advertisement] 
ADD CONSTRAINT FK_Category_Adsvertise FOREIGN KEY (CategoryId) REFERENCES [Category](CategoryId);

go 
ALTER TABLE [Report] 
ADD CONSTRAINT FK_Report_Adsvertise FOREIGN KEY (AdsId) REFERENCES [Advertisement](AdsId);
go

ALTER TABLE [Report] 
ADD CONSTRAINT FK_Report_Seller FOREIGN KEY (SellerId) REFERENCES [Seller](SellerId);
go

ALTER TABLE [Report] 
ADD CONSTRAINT FK_Report_Agent FOREIGN KEY (AgentId) REFERENCES [Agent](AgentId);

go 
ALTER TABLE [Image] 
ADD CONSTRAINT FK_Image_Adsvertise FOREIGN KEY (AdsId) REFERENCES [Advertisement](AdsId);

go 
ALTER TABLE [Agent] 
ADD CONSTRAINT FK_Agent_User FOREIGN KEY (UserId) REFERENCES [User](UserId);

go 
ALTER TABLE [Seller] 
ADD CONSTRAINT FK_Seller_User FOREIGN KEY (UserId) REFERENCES [User](UserId);

go 
ALTER TABLE [Agent] 
ADD CONSTRAINT FK_Payment_Agent FOREIGN KEY (paymentId) REFERENCES [Payment](PaymentId);


go





select *from Advertisement
select* from Image
select * from Agent
select * from [User]
select *from Seller