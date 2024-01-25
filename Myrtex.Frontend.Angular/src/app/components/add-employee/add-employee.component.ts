import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ModalService } from '../../services/modal.service';
import { EmployeesService } from '../../services/employees.service';
import { IDepartment, IEditEmployee } from '../../models/models';
import { DepartmentsService } from '../../services/department.service';
import { CommonModule } from '@angular/common';
import { FocusDirective } from '../../directives/focus.directive';

@Component({
  selector: 'app-add-employee',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule, FocusDirective],
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.scss',
})
export class AddEmployeeComponent implements OnInit{

@Output() employeeAdded: EventEmitter<void> = new EventEmitter<void>();

employee: IEditEmployee
departments: IDepartment[] = []

form = new FormGroup({
  firstName: new FormControl<string>('',[
    Validators.required,
    Validators.minLength(4)
  ]),
  lastName: new FormControl<string>('',[
    Validators.required,
    Validators.minLength(4)
  ]),
  middleName: new FormControl<string>('', [
    Validators.minLength(4)
  ]),
  birthDate: new FormControl<string | null>(null, [
    Validators.required
  ]),
  departmentId: new FormControl<number | null>(null, [
    Validators.required
  ]),
  salary: new FormControl<number | null>(null),
})

get fisrtName(){
  return this.form.controls.firstName as FormControl
}
get lastName(){
  return this.form.controls.lastName as FormControl
}
get middleName(){
  return this.form.controls.middleName as FormControl
}
get birthDate(){
  return this.form.controls.birthDate as FormControl
}
get departmentId(){
  return this.form.controls.departmentId as FormControl
}

constructor(
  public modalService: ModalService,
  private employeesService: EmployeesService,
  private departmentsService: DepartmentsService
){

}

  ngOnInit(): void {
    this.departmentsService.getDepartments().subscribe(response => {
      this.departments = response.departments
    })
  }

  submit(){
    Object.keys(this.form.controls).forEach(key => {
      const control = this.form.get(key);
      control?.markAsTouched();
    });
    if (this.form.valid) {
      this.employee = {
        firstName: this.form.value.firstName || '',
        lastName: this.form.value.lastName || '',
        middleName: this.form.value.middleName || '',
        birthDate: this.form.value.birthDate || '',
        departmentId: this.form.value.departmentId || 0,
        salary: this.form.value.salary || 0,
      };
  
      this.employeesService.addEmployee(this.employee).subscribe(() => {
        this.employeeAdded.emit();
        this.modalService.close();
      });
    }
  }
}
