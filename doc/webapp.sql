/*
SQLyog Ultimate v11.24 (32 bit)
MySQL - 5.7.20 : Database - webapp
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`webapp` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `webapp`;

/*Table structure for table `apploginlog` */

DROP TABLE IF EXISTS `apploginlog`;

CREATE TABLE `apploginlog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Uid` int(11) NOT NULL,
  `Ip` varchar(255) NOT NULL,
  `UserAgent` text,
  `Country` varchar(255) DEFAULT NULL,
  `City` varchar(255) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `apploginlog` */

/*Table structure for table `appuser` */

DROP TABLE IF EXISTS `appuser`;

CREATE TABLE `appuser` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(255) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `AliasName` varchar(255) NOT NULL,
  `PhoneNumber` varchar(255) DEFAULT NULL,
  `MailAddress` varchar(255) DEFAULT NULL,
  `IdCardNumber` varchar(255) DEFAULT NULL,
  `IdCardImageUri` varchar(255) DEFAULT NULL,
  `IsDisabled` bit(1) NOT NULL,
  `LastLoginTime` datetime NOT NULL,
  `CreateTime` datetime NOT NULL,
  `LastUpdate` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `appuser` */

/*Table structure for table `aspnetloginlog` */

DROP TABLE IF EXISTS `aspnetloginlog`;

CREATE TABLE `aspnetloginlog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(255) NOT NULL,
  `ClientIp` varchar(255) NOT NULL,
  `ClientCountry` varchar(255) DEFAULT NULL,
  `ClientProvince` varchar(255) DEFAULT NULL,
  `ClientCity` varchar(255) DEFAULT NULL,
  `UserAgent` text,
  `CreateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `aspnetloginlog` */

/*Table structure for table `aspnetrole` */

DROP TABLE IF EXISTS `aspnetrole`;

CREATE TABLE `aspnetrole` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `AliasName` varchar(255) NOT NULL,
  `AuthorityId` varchar(36) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `LastUpdate` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Data for the table `aspnetrole` */

insert  into `aspnetrole`(`Id`,`Name`,`AliasName`,`AuthorityId`,`CreateTime`,`LastUpdate`) values (9,'Admin','系统管理员','94b24901c98e4583b2c6913d4df97457','2018-03-01 10:04:26','2018-03-01 10:04:26'),(10,'Manager','主管','c938da276de14d75b498cdad2b64b474','2018-03-01 10:04:27','2018-03-01 10:04:27'),(11,'Operational','运营者','476300d679c14dc49f271757ee0b01e0','2018-03-01 10:04:27','2018-03-01 10:04:27'),(12,'User','用户','44db0f5aa73048da9783728c16902249','2018-03-01 10:04:27','2018-03-01 10:04:27');

/*Table structure for table `aspnetuser` */

DROP TABLE IF EXISTS `aspnetuser`;

CREATE TABLE `aspnetuser` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(255) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `GoogleAuthSecretKey` varchar(255) DEFAULT NULL,
  `GooleAuthEnabled` bit(1) NOT NULL,
  `AuthorityId` varchar(36) NOT NULL,
  `IsDisabled` bit(1) NOT NULL,
  `LastLoginTime` datetime NOT NULL,
  `CreateTime` datetime NOT NULL,
  `LastUpdate` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `aspnetuser` */

insert  into `aspnetuser`(`Id`,`UserName`,`PasswordHash`,`GoogleAuthSecretKey`,`GooleAuthEnabled`,`AuthorityId`,`IsDisabled`,`LastLoginTime`,`CreateTime`,`LastUpdate`) values (1,'admin','21232F297A57A5A743894A0E4A801FC3',NULL,'\0','46596303ec804259b1870ff0c8b5238a','\0','0001-01-01 00:00:00','2018-03-01 10:04:27','2018-03-01 10:04:27'),(2,'test','098F6BCD4621D373CADE4E832627B4F6',NULL,'\0','d5bf774e-63d1-4147-8e11-319fc1ac26ef','','0001-01-01 00:00:00','2018-03-02 13:43:50','0001-01-01 00:00:00');

/*Table structure for table `aspnetuserrole` */

DROP TABLE IF EXISTS `aspnetuserrole`;

CREATE TABLE `aspnetuserrole` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `aspnetuserrole` */

insert  into `aspnetuserrole`(`Id`,`UserId`,`RoleId`,`CreateTime`) values (1,1,9,'2018-03-02 13:07:53'),(2,2,9,'2018-03-02 13:43:50'),(3,2,9,'2018-03-02 13:44:00');

/*Table structure for table `authority` */

DROP TABLE IF EXISTS `authority`;

CREATE TABLE `authority` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AuthorityId` varchar(36) NOT NULL,
  `OperationId` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `authority` */

/*Table structure for table `interfaceoperation` */

DROP TABLE IF EXISTS `interfaceoperation`;

CREATE TABLE `interfaceoperation` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `ParentName` varchar(255) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Data for the table `interfaceoperation` */

insert  into `interfaceoperation`(`Id`,`GroupName`,`Name`,`Description`,`ParentName`,`CreateTime`) values (1,'用户/权限管理','ManageRoleAuthorities','角色权限',NULL,'2018-03-01 10:05:53'),(2,'用户/权限管理','read','读取','ManageRoleAuthorities','2018-03-01 10:05:53'),(3,'用户/权限管理','write','写入','ManageRoleAuthorities','2018-03-01 10:05:53'),(4,'业务用户管理','ManageAppUser','用户列表',NULL,'2018-03-01 10:05:53'),(5,'业务用户管理','read','读取','ManageAppUser','2018-03-01 10:05:53'),(6,'业务用户管理','ManageAppLoginLog','登录日志',NULL,'2018-03-01 10:05:54'),(7,'业务用户管理','read','读取','ManageAppLoginLog','2018-03-01 10:05:54'),(8,'用户/权限管理','ManageUser','用户管理',NULL,'2018-03-01 10:05:54'),(9,'用户/权限管理','read','读取','ManageUser','2018-03-01 10:05:54'),(10,'用户/权限管理','write','写入','ManageUser','2018-03-01 10:05:54'),(11,'用户/权限管理','delete','删除','ManageUser','2018-03-01 10:05:54'),(12,'用户/权限管理','set_user_authority','设置用户权限','ManageUser','2018-03-01 10:05:54');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
