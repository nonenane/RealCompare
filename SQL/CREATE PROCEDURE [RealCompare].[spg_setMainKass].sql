SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G.Y.
-- Create date: 2020-07-14
-- Description:	Запись\редактирование\удаление данных по главной кассе
-- =============================================
ALTER PROCEDURE [RealCompare].[spg_setMainKass]		 
	@id int,
	@date date,
	@MainKass numeric(16,2),
	@isVVO bit,
	@id_user int,
	@isDel bit,
	@result int

AS
BEGIN
	SET NOCOUNT ON;

BEGIN TRY 
	IF @isDel = 0
		BEGIN

			IF EXISTS (select TOP(1) id from [RealCompare].[j_MainKass] k where k.id <> @id and k.Data = @date and k.isVVO = @isVVO)
				BEGIN
					SELECT -1 as id;
					return;
				END

			IF EXISTS (select TOP(1) id from [RealCompare].[j_MainKass] k where k.id = @id and k.ChessBoard is not null and k.RealSQL is not null)
				BEGIN
					SELECT -2 as id;
					return;
				END

			IF @id = 0
				BEGIN
					INSERT INTO [RealCompare].[j_MainKass]  (Data,MainKass,isVVO,id_Creator,DateCreate,id_Editor,DateEdit)
					VALUES (@date,@MainKass,@isVVO,@id_user,GETDATE(),@id_user,GETDATE())

					SELECT  cast(SCOPE_IDENTITY() as int) as id
					return;
				END
			ELSE
				BEGIN
					UPDATE [RealCompare].[j_MainKass] 
					set		Data=@date,
							MainKass=@MainKass,
							isVVO= @isVVO,
							id_Editor=@id_user,
							DateEdit=GETDATE()
					where id = @id
										
					SELECT @id as id
					return;
				END
		END
	ELSE
		BEGIN
			IF @result = 0
				BEGIN
					
					IF NOT EXISTS(select TOP(1) id from [RealCompare].[j_MainKass] where id = @id)
						BEGIN
							select -1 as id
							return;
						END
					
					select 0 as id
					return;
				END
			ELSE
				BEGIN
					DELETE FROM [RealCompare].[j_MainKass]  where id = @id
					RETURN
				END
		END
END TRY 
BEGIN CATCH 
	SELECT -9999 as id
	return;
END CATCH
	
END
