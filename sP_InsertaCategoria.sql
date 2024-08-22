USE  BDCrudTest

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
 end catch
 end
go