

create database BDCrudTest

create table coCategoria(
nIdCategori int primary key identity(1,1) not null,
cNombCateg varchar(100) not null,
cEsActiva bit null
)

create table coProducto(
nIdProduct int primary key identity(1,1) not null,
cNombProdu varchar(100) not null,
nPrecioProd money not null,
nIdCategori int null,
CONSTRAINT fk_product_category FOREIGN KEY (nIdCategori) REFERENCES coCategoria (nIdCategori)
)

insert coCategoria
values ('Comestibles', 2)
insert coCategoria
values ('Aseo', 3)

select * from coCategoria

insert coProducto 
values ('Galletas', 250, 1)

insert coProducto 
values ('Pasta dental', 850, 2)

insert coProducto 
values ('Desinfectante', 700, 2)

insert coProducto 
values ('Alcohol en gel', 1250, 2)

insert coProducto 
values ('Arroz', 2600.35, 1)

insert coProducto 
values ('Frijoles', 1830.20, 1)

select * from coProducto


update coProducto
set nIdCategori = 3
where nIdProduct = 6




create procedure Usp_Sel_Co_Productos
@IdCategory int 
as
begin
 begin try
 set fmtonly off
 declare @message varchar(200), @inIdCategoria int;

    -- verifica existencia de categoria
	 set @inIdCategoria = (select nIdCategori from coCategoria where nIdCategori= @IdCategory)
	 If @inIdCategoria is not null 
	 begin 
		 select 
		  p.nIdProduct
		 ,p.cNombProdu
		 ,p.nPrecioProd
		 ,C.nIdCategori
		 ,C.cNombCateg 
		 from coProducto as P
		 left join coCategoria C on C.nIdCategori = P.nIdCategori
		 where P.nIdCategori = @IdCategory
	 end 
	 else  
	 begin
	   set @message='No se encontraron productos con esa categoria'
	   select @message
	 end
end try
begin catch
	select @message
	rollback 
	print error_message()
 end catch
 end
go 

create procedure Usp_Ins_Co_Categoria
 @NameCategory varchar(100)
,@Estado bit
as
begin
 begin try
 set fmtonly off
 declare @message varchar(200);

    -- verifica existencia de categoria
	 If  not exists (select nIdCategori from coCategoria where cNombCateg = @NameCategory)
	 begin 
		insert coCategoria
        values (@NameCategory, @Estado)
	 end 
	 else  
	 begin
    set @message='Ya exixte una categoria con la misma descripciòn '
	   select @message
	 end
end try
begin catch
	select @message
	rollback 
	print error_message()
 end catch
 end
go 