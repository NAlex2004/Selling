/*
create database Sales
on 
( NAME = Sales,
  FILENAME = 'E:\My\Private\CS\Training\Selling\DataModel\DB\Sales.mdf')
log on
( NAME = Sales_log,
  FILENAME = 'E:\My\Private\CS\Training\Selling\DataModel\DB\Sales.ldf')
  
*/

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

create table Sales
(
	CustomerId int not null,
	ProductId int not null,
	Total float not null default 0,
	constraint FK_Customer foreign key (CustomerID) references Customers(Id),
	constraint FK_Product foreign key (ProductID) references Products(Id)
)

create index Ind_CustomerName on Customers(CustomerName)
create unique index Ind_ProductName on Products(ProductName)
