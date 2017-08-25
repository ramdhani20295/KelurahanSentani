-- MySQL Script generated by MySQL Workbench
-- Fri Aug 25 01:44:04 2017
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema kependudukan
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema kependudukan
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `kependudukan` DEFAULT CHARACTER SET utf8 ;
USE `kependudukan` ;

-- -----------------------------------------------------
-- Table `kependudukan`.`users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`users` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`users` (
  `Id` VARCHAR(128) NOT NULL,
  `Email` VARCHAR(256) NULL DEFAULT NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` LONGTEXT NULL DEFAULT NULL,
  `SecurityStamp` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumber` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEndDateUtc` DATETIME NULL DEFAULT NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT(11) NOT NULL,
  `UserName` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`Pejabat`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`Pejabat` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`Pejabat` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `Nama` VARCHAR(255) NULL DEFAULT NULL,
  `Alamat` VARCHAR(255) NULL DEFAULT NULL,
  `Level` ENUM('RT', 'RW', 'Kelurahan') NULL DEFAULT NULL,
  `InstansiID` INT(11) NULL DEFAULT NULL,
  `Jabatan` ENUM('Ketua', 'Sekertaris') NULL DEFAULT NULL,
  `Status` ENUM('true', 'false') NULL DEFAULT NULL,
  `usersId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `fk_pejabat_users1_idx` (`usersId` ASC),
  CONSTRAINT `fk_pejabat_users1`
    FOREIGN KEY (`usersId`)
    REFERENCES `kependudukan`.`users` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`RW`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`RW` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`RW` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `NoRW` VARCHAR(4) NULL DEFAULT NULL,
  `PejabatId` INT(11) NOT NULL,
  PRIMARY KEY (`Id`, `PejabatId`),
  INDEX `fk_RW_Pejabat1_idx` (`PejabatId` ASC),
  CONSTRAINT `fk_RW_Pejabat1`
    FOREIGN KEY (`PejabatId`)
    REFERENCES `kependudukan`.`Pejabat` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`RT`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`RT` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`RT` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `NoRT` VARCHAR(4) NULL DEFAULT NULL,
  `RWId` INT(4) NULL DEFAULT NULL,
  `PejabatId` INT(11) NOT NULL,
  PRIMARY KEY (`Id`, `PejabatId`),
  INDEX `RWId` (`RWId` ASC),
  INDEX `fk_RT_Pejabat1_idx` (`PejabatId` ASC),
  CONSTRAINT `rt_ibfk_1`
    FOREIGN KEY (`RWId`)
    REFERENCES `kependudukan`.`RW` (`Id`),
  CONSTRAINT `fk_RT_Pejabat1`
    FOREIGN KEY (`PejabatId`)
    REFERENCES `kependudukan`.`Pejabat` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`KartuKeluarga`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`KartuKeluarga` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`KartuKeluarga` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `NoKK` VARCHAR(20) NULL DEFAULT NULL,
  `Alamat` VARCHAR(255) NULL DEFAULT NULL,
  `KodePos` VARCHAR(5) NULL DEFAULT NULL,
  `RT_Id` INT(11) NOT NULL,
  PRIMARY KEY (`Id`, `RT_Id`),
  INDEX `fk_KartuKeluarga_RT1_idx` (`RT_Id` ASC),
  CONSTRAINT `fk_KartuKeluarga_RT1`
    FOREIGN KEY (`RT_Id`)
    REFERENCES `kependudukan`.`RT` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`Penduduk`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`Penduduk` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`Penduduk` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `NIK` VARCHAR(20) NOT NULL DEFAULT '',
  `Nama` VARCHAR(50) NULL DEFAULT NULL,
  `TempatLahir` VARCHAR(255) NULL DEFAULT NULL,
  `TanggalLahir` DATE NULL DEFAULT NULL,
  `Agama` ENUM('Islam', 'Protestan', 'Khatolik', 'Hindu', 'Budha', 'Konghuchu') NULL DEFAULT NULL,
  `JK` ENUM('Pria', 'Wanita') NULL DEFAULT NULL,
  `Pekerjaan` VARCHAR(255) NULL DEFAULT NULL,
  `Pendidikan` ENUM('SD', 'SMP/Sederajat', 'SMA/Sederajat', 'S1', 'S2', 'S3') NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `NIK` (`NIK` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`PendudukDetail`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`PendudukDetail` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`PendudukDetail` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `StatusPerkawinan` ENUM('Kawin', 'Belum Kawin') NULL DEFAULT NULL,
  `HubunganDalamKeluarga` VARCHAR(255) NULL DEFAULT NULL,
  `Kewarganegaraan` ENUM('WNI', 'WNA') NULL DEFAULT NULL,
  `Dokumen` ENUM('Pasport', 'KITAS', 'KITAP') NULL DEFAULT NULL,
  `Ayah` VARCHAR(255) NULL DEFAULT NULL,
  `Ibu` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`roles`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`roles` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`roles` (
  `Id` VARCHAR(128) NOT NULL,
  `Name` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`JenisSurat`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`JenisSurat` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`JenisSurat` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `KodeSurat` VARCHAR(255) NULL DEFAULT NULL,
  `JenisSurat` VARCHAR(255) NULL DEFAULT NULL,
  `FormatSurat` TEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`Surat`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`Surat` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`Surat` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `JenisSuratID` INT(11) NULL DEFAULT NULL,
  `NoSurat` VARCHAR(255) NULL DEFAULT NULL,
  `TanggalBuat` DATE NULL DEFAULT NULL,
  `PersonID` INT(11) NULL DEFAULT NULL,
  `BerlakuHingga` DATE NULL,
  PRIMARY KEY (`Id`),
  INDEX `PersonID` (`PersonID` ASC),
  INDEX `JenisSuratID` (`JenisSuratID` ASC),
  CONSTRAINT `detail_surat_ibfk_2`
    FOREIGN KEY (`JenisSuratID`)
    REFERENCES `kependudukan`.`JenisSurat` (`Id`),
  CONSTRAINT `detail_surat_ibfk_1`
    FOREIGN KEY (`PersonID`)
    REFERENCES `kependudukan`.`Penduduk` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`Persetujuan`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`Persetujuan` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`Persetujuan` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `IdSurat` INT(11) NULL DEFAULT NULL,
  `IdPejabat` INT(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IdSurat` (`IdSurat` ASC),
  INDEX `IdPejabat` (`IdPejabat` ASC),
  CONSTRAINT `persetujuan_ibfk_2`
    FOREIGN KEY (`IdPejabat`)
    REFERENCES `kependudukan`.`Pejabat` (`Id`),
  CONSTRAINT `persetujuan_ibfk_1`
    FOREIGN KEY (`IdSurat`)
    REFERENCES `kependudukan`.`Surat` (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`userroles`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`userroles` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`userroles` (
  `UserId` VARCHAR(128) NOT NULL,
  `RoleId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`),
  INDEX `IdentityRole_Users` (`RoleId` ASC),
  CONSTRAINT `ApplicationUser_Roles`
    FOREIGN KEY (`UserId`)
    REFERENCES `kependudukan`.`users` (`Id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `IdentityRole_Users`
    FOREIGN KEY (`RoleId`)
    REFERENCES `kependudukan`.`roles` (`Id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`userlogins`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`userlogins` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`userlogins` (
  `LoginProvider` VARCHAR(128) NOT NULL,
  `ProviderKey` VARCHAR(128) NOT NULL,
  `UserId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`, `ProviderKey`, `UserId`),
  INDEX `ApplicationUser_Logins` (`UserId` ASC),
  CONSTRAINT `ApplicationUser_Logins`
    FOREIGN KEY (`UserId`)
    REFERENCES `kependudukan`.`users` (`Id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`userclaims`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`userclaims` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`userclaims` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(128) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id` (`Id` ASC),
  INDEX `UserId` (`UserId` ASC),
  CONSTRAINT `ApplicationUser_Claims`
    FOREIGN KEY (`UserId`)
    REFERENCES `kependudukan`.`users` (`Id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `kependudukan`.`KKDetail`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `kependudukan`.`KKDetail` ;

CREATE TABLE IF NOT EXISTS `kependudukan`.`KKDetail` (
  `KartuKeluarga_Id` INT(11) NOT NULL,
  `Penduduk_Id` INT(11) NOT NULL,
  `PendudukDetail_Id` INT(11) NOT NULL,
  INDEX `fk_KKDetail_KartuKeluarga1_idx` (`KartuKeluarga_Id` ASC),
  INDEX `fk_KKDetail_Penduduk1_idx` (`Penduduk_Id` ASC),
  INDEX `fk_KKDetail_PendudukDetail1_idx` (`PendudukDetail_Id` ASC),
  CONSTRAINT `fk_KKDetail_KartuKeluarga1`
    FOREIGN KEY (`KartuKeluarga_Id`)
    REFERENCES `kependudukan`.`KartuKeluarga` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_KKDetail_Penduduk1`
    FOREIGN KEY (`Penduduk_Id`)
    REFERENCES `kependudukan`.`Penduduk` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_KKDetail_PendudukDetail1`
    FOREIGN KEY (`PendudukDetail_Id`)
    REFERENCES `kependudukan`.`PendudukDetail` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;