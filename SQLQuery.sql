if object_id('Country','u') is null 
begin
create table Country(
id int primary key identity(1,1),
name varchar(100)
)
end
go
if object_id('State','u')is null
begin
create table State(
id int primary key identity(1,1),
name varchar(100),
country_id int references Country(id)
)
end
go
if object_id ('city','u') is null
begin
create table city(
id int primary key identity(1,1),
name varchar(100),
state_id int references State(id)
)
end
go

create or alter procedure Usp_get_countrys
as
begin
select * from Country;
end
go
create or alter procedure uap_Get_states(@id int)
as
begin
select * from State where country_id=@id;
end
go
create or alter procedure uap_Get_cityes(@id int)
as
begin
select * from city where state_id=@id;
end
go
if object_id('Csc','u') is null
begin
create table csc(
id int primary key identity(1,1),
country_id int references Country(id),
state_id int references State(id),
city_id int references city(id)
)
end
go
create or alter procedure usp_save_csc(@country_id int,
     @state_id int,
	 @city_id int,
	 @id int=null)
	 as
begin
if(@id>0)
begin
update csc set country_id=@country_id,state_id=@state_id,city_id=@city_id where id=@id;
end
else
begin
insert into csc(country_id,state_id,city_id) values(@country_id,@state_id,@city_id)
end
end
go
create or alter procedure usp_get_cscs
as
begin
 select csc.*,Country.name as countryname,State.name as statename	,city.name as cityname from csc join Country on Country.id=csc.country_id join State on State.id=csc.state_id join city on city.id=csc.city_id
 end
 go
 create or alter procedure usp_del_csc(@id int)
 as
 begin
 delete csc where id=@id
 end
 go
 create or alter procedure usp_get_csc(@id int)
 as
 begin
 select * from csc where id=@id
  end