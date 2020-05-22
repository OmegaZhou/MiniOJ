use oj_db;
drop table if exists submission;
drop table if exists user;
drop table if exists problem;

create table user(
    `UserId` Integer AUTO_INCREMENT,
    `Nickname` varchar(50) unique,
    `Password` varchar(50),
    primary key(`UserId`)
);
create table problem(
    `ProblemId` integer AUTO_INCREMENT,
    `Title` varchar(50) unique,
    `MaxTime` integer,
    `MaxMemory` integer,
    primary key(`ProblemId`)
);

create table submission(
    `SubmissionId` integer AUTO_INCREMENT,
    `UserId` integer,
    `ProblemId` integer,
    `Status` varchar(20),
    `Code` text,
    `Time` integer,
    `Memory` integer,
    `Lang` varchar(10),
    `Uuid` varchar(40),
    `CreateTime` datetime default CURRENT_TIMESTAMP,
    primary key(`SubmissionId`),
    foreign key (`UserId`) references user(`UserId`) on delete cascade on update cascade,
    foreign key (`ProblemId`) references problem(`ProblemId`) on delete cascade on update cascade
);

