insert into Student values(190,"ABC");
insert into Student values(191,"XYZ");
insert into Student values(192,"MNO");
insert into Student values(193,"PQR");



-- PLaced students
-- select s.roll_number, name, package, company_name from Student s right join Placed p on s.roll_number = p.roll_number left join Companies c on c.company_id=p.company_id;

-- unplaced students
-- select s.roll_number, name from Student s left join Placed p on s.roll_number = p.roll_number  where company_id is NULL;

-- View companies in which student x sit;
-- select company_name, package from AllowedStudents a left join Companies c on a.company_id=c.company_id where student_roll_no={rollNumber}
