CREATE USER 'game_user'@'%' IDENTIFIED BY 'game_password';
CREATE DATABASE IF NOT EXISTS `game_db`;

USE `game_db`;

CREATE TABLE `account_info` (
  `player_id` bigint NOT NULL AUTO_INCREMENT COMMENT '플레이어 아이디',
  `email` varchar(50) NOT NULL COMMENT '이메일',
  `salt_value` varchar(100) NOT NULL COMMENT '암호화 값',
  `pw` varchar(100) NOT NULL COMMENT '해싱된 비밀번호',
  `create_dt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '생성 일시',
  `recent_login_dt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '최근 로그인 일시',
  PRIMARY KEY (`player_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `user` (
  `uid` int NOT NULL AUTO_INCREMENT COMMENT '유저아이디',
  `nickname` varchar(50) NOT NULL COMMENT '닉네임',
  `create_dt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '생성 일시',
  `recent_login_dt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '최근 로그인 일시',
  `wins` int unsigned NOT NULL DEFAULT '0',
  `loses` int unsigned NOT NULL DEFAULT '0',
  `player_id` bigint NOT NULL,
  PRIMARY KEY (`uid`),
  UNIQUE KEY `nickname` (`nickname`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `friend` (
  `uid` int NOT NULL COMMENT '유저아이디',
  `friend_uid` int NOT NULL COMMENT '친구 유저아이디',
  `friend_yn` tinyint NOT NULL DEFAULT '0' COMMENT '친구 여부',
  `create_dt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '생성 일시',
  PRIMARY KEY (`uid`,`friend_uid`),
  KEY `FK_friend_friend_uid_user_uid` (`friend_uid`),
  CONSTRAINT `FK_friend_friend_uid_user_uid` FOREIGN KEY (`friend_uid`) REFERENCES `user` (`uid`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_friend_uid_user_uid` FOREIGN KEY (`uid`) REFERENCES `user` (`uid`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `game_move_record` (
  `game_id` int NOT NULL,
  `count` int NOT NULL,
  `piece` varchar(100) NOT NULL,
  `from` varchar(100) NOT NULL,
  `to` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `game_room` (
  `game_id` int NOT NULL AUTO_INCREMENT,
  `white_uid` int NOT NULL,
  `black_uid` int NOT NULL,
  `is_finished` tinyint DEFAULT NULL,
  PRIMARY KEY (`game_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `mailbox` (
  `mail_seq` int NOT NULL AUTO_INCREMENT COMMENT '우편 일련번호',
  `uid` int NOT NULL COMMENT '유저아이디',
  `mail_title` varchar(100) NOT NULL COMMENT '우편 제목',
  `create_dt` datetime NOT NULL COMMENT '생성 일시',
  `expire_dt` datetime NOT NULL COMMENT '만료 일시',
  `receive_dt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '수령 일시',
  `receive_yn` tinyint NOT NULL DEFAULT '0' COMMENT '수령 유무',
  PRIMARY KEY (`mail_seq`),
  KEY `FK_mailbox_uid_user_uid` (`uid`),
  CONSTRAINT `FK_mailbox_uid_user_uid` FOREIGN KEY (`uid`) REFERENCES `user` (`uid`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `mailbox_reward` (
  `mail_seq` int NOT NULL COMMENT '우편 일련번호',
  `reward_key` int NOT NULL COMMENT '보상 키',
  `reward_qty` int NOT NULL COMMENT '보상 수',
  `reward_type` varchar(20) NOT NULL COMMENT '보상 타입',
  PRIMARY KEY (`mail_seq`,`reward_key`),
  CONSTRAINT `FK_mailbox_reward_mail_seq_mailbox_mail_seq` FOREIGN KEY (`mail_seq`) REFERENCES `mailbox` (`mail_seq`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `master_attendance_reward` (
  `day_seq` int NOT NULL COMMENT '날짜 번호',
  `reward_key` int NOT NULL COMMENT '보상 키',
  `reward_qty` int NOT NULL DEFAULT '0' COMMENT '보상 수',
  `reward_type` varchar(20) NOT NULL COMMENT '보상 종류',
  `create_dt` datetime NOT NULL COMMENT '생성 일시',
  PRIMARY KEY (`day_seq`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `user_attendance` (
  `uid` int NOT NULL COMMENT '유저아이디',
  `attendance_cnt` int NOT NULL COMMENT '출석 횟수',
  `recent_attendance_dt` datetime NOT NULL COMMENT '최근 출석 일시',
  PRIMARY KEY (`uid`),
  CONSTRAINT `FK_user_attendance_uid_user_uid` FOREIGN KEY (`uid`) REFERENCES `user` (`uid`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



CREATE DATABASE IF NOT EXISTS `master_db`;
USE `master_db`;

CREATE TABLE `version` (
  `app_version` varchar(10) NOT NULL COMMENT '앱 버전',
  `master_data_version` varchar(10) NOT NULL COMMENT '마스터 데이터 버전'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

LOCK TABLES `version` WRITE;
INSERT into `version` values ('0.1', '0.1');
UNLOCK TABLES;


GRANT ALL PRIVILEGES ON `game_db`.* TO 'game_user'@'%';
GRANT ALL PRIVILEGES ON `master_db`.* TO 'game_user'@'%';

FLUSH PRIVILEGES;