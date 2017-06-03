
create database Sales
on 
( NAME = Sales,
  FILENAME = 'E:\My\Private\CS\Training\Selling\DataModel\DB\Sales.mdf')
log on
( NAME = Sales_log,
  FILENAME = 'E:\My\Private\CS\Training\Selling\DataModel\DB\Sales.ldf')
go 

use Sales
go

create table Customers
(
	Id int identity primary key,
	CustomerName varchar(255) not null
)

create table Products
(
	Id int identity primary key,
	ProductName varchar(255) not null,
	Price float not null default 0
)


create table Managers
(
	Id int not null identity primary key,
	LastName varchar(100) not null unique,	
)

create table Sales
(
	Id int not null identity primary key,
	SaleDate datetime not null,
	ManagerId int not null,
	CustomerId int not null,
	ProductId int not null,
	Total float not null default 0,
	constraint FK_Customer foreign key (CustomerID) references Customers(Id),
	constraint FK_Product foreign key (ProductID) references Products(Id),
	constraint FK_Manager foreign key (ManagerId) references Managers(Id)
)

create table TempSales
( 
	SessionId uniqueidentifier not null primary key,
	SaleDate datetime not null,
	ManagerName varchar(100) not null,
	CustomerName varchar(255) not null,
	ProductName varchar(255) not null,
	Total float not null default 0,	
)

create index Ind_CustomerName on Customers(CustomerName)
create unique index Ind_ProductName on Products(ProductName)
