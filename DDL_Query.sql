CREATE TABLE mst_cabang (
	kode_cabang VARCHAR(50) PRIMARY KEY NOT NULL,
	nama_cabang VARCHAR(255)
);

CREATE TABLE mst_jabatan (
	kode_jabatan VARCHAR(50) PRIMARY KEY NOT NULL,
	nama_jabatan VARCHAR(255)
);

CREATE TABLE mst_pegawai (
	kode_pegawai VARCHAR(50) PRIMARY KEY NOT NULL,
	kode_cabang VARCHAR(50) NOT NULL,
	kode_jabatan VARCHAR(50) NOT NULL,
	nama_pegawai VARCHAR(255),
	start_contract DATE,
	end_contract DATE,

	CONSTRAINT fk_pegawai_kode_cabang FOREIGN KEY (kode_cabang)
		REFERENCES mst_cabang(kode_cabang)
		ON DELETE CASCADE,
	CONSTRAINT fk_pegawai_kode_jabatan FOREIGN KEY (kode_jabatan)
		REFERENCES mst_jabatan(kode_jabatan)
		ON DELETE CASCADE
);

-- DROP PROCEDURE GetPegawai;

-- EXEC GetPegawai @start_date='2024-08-05', @end_date='2026-08-05';