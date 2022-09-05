drop database project2;
create database project2;
use project2;

create table if not exists Student (
 roll_number bigint primary key,
 name Varchar (50) not null
);

create table if not exists Companies (
 company_id int primary key AUTO_INCREMENT,
 company_name varchar(50) unique,
 package float
);

create table if not exists Placed (
 roll_number bigint primary key,
 company_id int,
 foreign key (roll_number) references Student(roll_number),
 foreign key (company_id) references Companies(company_id)
);

create table if not exists AllowedStudents (
 company_id int,
 student_name Varchar(50),
 student_roll_no bigint,
 CONSTRAINT Pk PRIMARY KEY (company_id,student_roll_no),
 foreign key (company_id) references Companies(company_id),
 foreign key (student_roll_no) references Student(roll_number)
);
