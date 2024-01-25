select * from employees;

go

select * from employees where salary > 10000;

go

delete from users where datediff(year, BirthDate, getdate()) > 70;

go

update employees set salary = 15000 where salary < 15000;

go