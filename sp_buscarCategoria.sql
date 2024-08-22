
create procedure Usp_Sel_Co_Productos
@IdCategory int 
as
begin
 begin try
 set fmtonly off
 declare @message varchar(200), @inIdCategoria int;

    -- verifica existencia de categoria
	 set @inIdCategoria = (select nIdCategori from coCategoria where nIdCategori = @IdCategory)
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
 end catch
 end
go