CREATE PROCEDURE GetPegawai
	@start_date DATE = NULL,
	@end_date DATE = NULL
AS
BEGIN
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
	WHERE
		(
			@start_date IS NOT NULL AND @end_date IS NULL AND p.start_contract >= @start_date
		)
		OR
		(
			@start_date IS NULL AND @end_date IS NOT NULL AND p.end_contract <= @end_date
		)
		OR
		(
			@start_date IS NOT NULL AND @end_date IS NOT NULL AND p.start_contract BETWEEN @start_date AND @end_date
		)
		OR
		(
			@start_date IS NULL AND @end_date IS NULL
		)
END;