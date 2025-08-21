CREATE PROCEDURE GetPegawai
	@start_date DATE = NULL,
	@end_date DATE = NULL
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX) = '
	SELECT
		p.kode_pegawai,
		p.nama_pegawai,
		c.kode_cabang,
		c.nama_cabang,
		j.kode_jabatan,
		j.nama_jabatan,
		p.start_contract,
		p.end_contract
	FROM
		mst_pegawai p
			JOIN mst_cabang c ON c.kode_cabang = p.kode_cabang
			JOIN mst_jabatan j ON j.kode_jabatan = p.kode_jabatan
		WHERE 1=1';
		
	IF @start_date IS NOT NULL
		SET @sql += ' AND p.start_contract >= @start_date';

	IF @end_date IS NOT NULL AND @start_date IS NOT NULL
		SET @sql += ' AND p.end_contract <= @end_date';
	ELSE IF @end_date IS NOT NULL
		SET @sql += ' AND p.end_contract <= @end_date AND p.end_contract >= CAST(GETDATE() AS DATE)'

	EXEC sp_executesql @sql,
		N'@start_date DATE, @end_date DATE',
		@start_date = @start_date,
		@end_date = @end_date;
END;