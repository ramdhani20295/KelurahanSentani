# Host: localhost  (Version 5.6.14)
# Date: 2017-09-10 03:27:26
# Generator: MySQL-Front 6.0  (Build 2.20)


#
# Structure for table "jenissurat"
#

DROP TABLE IF EXISTS `jenissurat`;
CREATE TABLE `jenissurat` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `KodeSurat` varchar(255) DEFAULT NULL,
  `JenisSurat` varchar(255) DEFAULT NULL,
  `FormatSurat` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "jenissurat"
#


#
# Structure for table "kartukeluarga"
#

DROP TABLE IF EXISTS `kartukeluarga`;
CREATE TABLE `kartukeluarga` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NoKK` varchar(20) DEFAULT NULL,
  `Alamat` varchar(255) DEFAULT NULL,
  `RTId` int(11) DEFAULT NULL,
  `Tanggal` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `NoKK` (`NoKK`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

#
# Data for table "kartukeluarga"
#

INSERT INTO `kartukeluarga` VALUES (7,'123456789','Jln. Raya Sentani',3,'2017-09-09 02:53:00');

#
# Structure for table "penduduk"
#

DROP TABLE IF EXISTS `penduduk`;
CREATE TABLE `penduduk` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NIK` varchar(20) NOT NULL DEFAULT '',
  `Nama` varchar(50) DEFAULT NULL,
  `TempatLahir` varchar(255) DEFAULT NULL,
  `TanggalLahir` date DEFAULT NULL,
  `Agama` enum('Islam','Protestan','Khatolik','Hindu','Budha','Konghuchu') NOT NULL DEFAULT 'Islam',
  `JK` enum('Pria','Wanita') NOT NULL DEFAULT 'Pria',
  `Pekerjaan` varchar(255) DEFAULT NULL,
  `Pendidikan` enum('SD','SMP','SMA','S1','S2','S3') NOT NULL DEFAULT 'SMA',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `NIK` (`NIK`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

#
# Data for table "penduduk"
#

INSERT INTO `penduduk` VALUES (3,'987654321','Yoseph Kungkung','Palopo','2017-09-15','Protestan','Pria','Petani','S1'),(5,'123','Elish','asdasd','2017-09-12','Protestan','Wanita','asdwa','S1'),(6,'123123','asdasda','as aasd','2017-09-14','Khatolik','Wanita','asdwa','SD'),(7,'12333','asdasdw','asdwa','0001-01-01','Protestan','Wanita','sadwa','SD');

#
# Structure for table "kkdetail"
#

DROP TABLE IF EXISTS `kkdetail`;
CREATE TABLE `kkdetail` (
  `KartuKeluargaId` int(11) NOT NULL,
  `PendudukId` int(11) NOT NULL,
  KEY `fk_KKDetail_KartuKeluarga1_idx` (`KartuKeluargaId`),
  KEY `fk_KKDetail_Penduduk1_idx` (`PendudukId`),
  CONSTRAINT `fk_KKDetail_KartuKeluarga1` FOREIGN KEY (`KartuKeluargaId`) REFERENCES `kartukeluarga` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_KKDetail_Penduduk1` FOREIGN KEY (`PendudukId`) REFERENCES `penduduk` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

#
# Data for table "kkdetail"
#

INSERT INTO `kkdetail` VALUES (7,3),(7,5),(7,6),(7,7);

#
# Structure for table "pendudukdetail"
#

DROP TABLE IF EXISTS `pendudukdetail`;
CREATE TABLE `pendudukdetail` (
  `Id` int(11) NOT NULL DEFAULT '0',
  `StatusPerkawinan` enum('Kawin','Belum') NOT NULL DEFAULT 'Belum',
  `HubunganDalamKeluarga` enum('KepalaKeluarga','Istri','Anak','Ibu','Bapak','Famili') NOT NULL DEFAULT 'KepalaKeluarga',
  `Kewarganegaraan` enum('WNI','WNA') NOT NULL DEFAULT 'WNI',
  `Ayah` varchar(255) DEFAULT NULL,
  `Ibu` varchar(255) DEFAULT NULL,
  `Paspor` varchar(255) DEFAULT NULL,
  `DokumenLain` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "pendudukdetail"
#

INSERT INTO `pendudukdetail` VALUES (3,'Kawin','KepalaKeluarga','WNI',NULL,NULL,NULL,''),(5,'Kawin','Istri','WNI','Pace','Kambuno','',''),(6,'Belum','Anak','WNI','dwadsad ddd','aaaaaa',NULL,NULL),(7,'Kawin','Famili','WNA','dw','a','asdwa','asd');

#
# Structure for table "permohonan"
#

DROP TABLE IF EXISTS `permohonan`;
CREATE TABLE `permohonan` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "permohonan"
#


#
# Structure for table "roles"
#

DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "roles"
#

INSERT INTO `roles` VALUES ('1','Administrator'),('d44ab7ce-0ca6-41eb-8b1b-97a18cf64deb','Pejabat');

#
# Structure for table "surat"
#

DROP TABLE IF EXISTS `surat`;
CREATE TABLE `surat` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NoSurat` varchar(255) DEFAULT NULL,
  `TanggalBuat` date DEFAULT NULL,
  `PersonID` int(11) DEFAULT NULL,
  `BerlakuHingga` date DEFAULT NULL,
  `JenisSuratId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `PersonID` (`PersonID`),
  KEY `fk_surat_jenissurat1_idx` (`JenisSuratId`),
  CONSTRAINT `detail_surat_ibfk_1` FOREIGN KEY (`PersonID`) REFERENCES `penduduk` (`Id`),
  CONSTRAINT `fk_surat_jenissurat1` FOREIGN KEY (`JenisSuratId`) REFERENCES `jenissurat` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "surat"
#


#
# Structure for table "users"
#

DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "users"
#

INSERT INTO `users` VALUES ('3a10609b-9c68-4f00-a604-72312f6404e5','kristt26@gmail.com',0,'AA/qhA5RKtrg11NVjJ0GUROYHsP94Gszr27xZYHqYJq6HoAhbQZ+T4/gSgGC4Xkh7Q==','c3cde312-f883-4eb4-90c5-b33c77db5748',NULL,0,0,'2017-09-08 07:37:30',1,0,'kristt26@gmail.com'),('ea05bc0a-b12b-48ce-a2ef-d827570195b3','ocph23@gmail.com',1,'AJUqAGH1TjvTSi33qTGj+llQE+bTrDae6bFqK9Op8HdZkIqjIFUODRsfPXk107khbw==','2e73aa0a-ecf3-4fb1-9e5f-8ab1c1e872ba',NULL,0,0,NULL,0,0,'ocph23@gmail.com');

#
# Structure for table "userroles"
#

DROP TABLE IF EXISTS `userroles`;
CREATE TABLE `userroles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IdentityRole_Users` (`RoleId`),
  CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "userroles"
#

INSERT INTO `userroles` VALUES ('3a10609b-9c68-4f00-a604-72312f6404e5','d44ab7ce-0ca6-41eb-8b1b-97a18cf64deb'),('ea05bc0a-b12b-48ce-a2ef-d827570195b3','1');

#
# Structure for table "userlogins"
#

DROP TABLE IF EXISTS `userlogins`;
CREATE TABLE `userlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `ApplicationUser_Logins` (`UserId`),
  CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "userlogins"
#


#
# Structure for table "userclaims"
#

DROP TABLE IF EXISTS `userclaims`;
CREATE TABLE `userclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `ApplicationUser_Claims` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "userclaims"
#


#
# Structure for table "pejabat"
#

DROP TABLE IF EXISTS `pejabat`;
CREATE TABLE `pejabat` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(255) DEFAULT NULL,
  `Alamat` varchar(255) DEFAULT NULL,
  `Level` enum('RT','RW','Kelurahan') DEFAULT NULL,
  `InstansiID` int(11) DEFAULT NULL,
  `Jabatan` enum('Ketua','Sekertaris') DEFAULT NULL,
  `Status` enum('true','false') DEFAULT NULL,
  `usersId` varchar(128) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_pejabat_users1_idx` (`usersId`),
  CONSTRAINT `fk_pejabat_users1` FOREIGN KEY (`usersId`) REFERENCES `users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;

#
# Data for table "pejabat"
#

INSERT INTO `pejabat` VALUES (13,'Ajenk Krisyanto Kungkung','Jln. Keluarhan','RW',0,'Ketua','true','3a10609b-9c68-4f00-a604-72312f6404e5');

#
# Structure for table "persetujuan"
#

DROP TABLE IF EXISTS `persetujuan`;
CREATE TABLE `persetujuan` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IdSurat` int(11) DEFAULT NULL,
  `IdPejabat` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdSurat` (`IdSurat`),
  KEY `IdPejabat` (`IdPejabat`),
  CONSTRAINT `persetujuan_ibfk_1` FOREIGN KEY (`IdSurat`) REFERENCES `surat` (`Id`),
  CONSTRAINT `persetujuan_ibfk_2` FOREIGN KEY (`IdPejabat`) REFERENCES `pejabat` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

#
# Data for table "persetujuan"
#


#
# Structure for table "rw"
#

DROP TABLE IF EXISTS `rw`;
CREATE TABLE `rw` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NoRW` varchar(4) DEFAULT NULL,
  `PejabatId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_RW_Pejabat1_idx` (`PejabatId`),
  CONSTRAINT `fk_RW_Pejabat1` FOREIGN KEY (`PejabatId`) REFERENCES `pejabat` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

#
# Data for table "rw"
#

INSERT INTO `rw` VALUES (5,'1',13);

#
# Structure for table "rt"
#

DROP TABLE IF EXISTS `rt`;
CREATE TABLE `rt` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NoRT` varchar(4) DEFAULT NULL,
  `RWId` int(4) DEFAULT NULL,
  `PejabatId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `RWId` (`RWId`),
  KEY `fk_RT_Pejabat1_idx` (`PejabatId`),
  CONSTRAINT `fk_RT_Pejabat1` FOREIGN KEY (`PejabatId`) REFERENCES `pejabat` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `rt_ibfk_1` FOREIGN KEY (`RWId`) REFERENCES `rw` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

#
# Data for table "rt"
#

INSERT INTO `rt` VALUES (3,'1',5,13);
