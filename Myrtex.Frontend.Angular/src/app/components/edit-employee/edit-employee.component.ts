import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IDepartment, IEditEmployee, IEmployee } from '../../models/models';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ModalService } from '../../services/modal.service';
import { EmployeesService } from '../../services/employees.service';
import { DepartmentsService } from '../../services/department.service';
import { FocusDirective } from '../../directives/focus.directive';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-employee',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule, FocusDirective],
  templateUrl: './edit-employee.component.html',
  styleUrl: './edit-employee.component.scss'
})
export class EditEmployeeComponent  implements OnInit{
  @Input() employee?: IEmployee
  @Output() employeeEdited: EventEmitter<void> = new EventEmitter<void>()
  
  editEmployee: IEditEmployee
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
    birthDate: new FormControl<string>('', [
      Validators.required
    ]),
    departmentId: new FormControl<number>(0, [
      Validators.required
    ]),
    salary: new FormControl<number>(0),
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
      if(this.employee){
        this.employeesService.getEmployeeToEdit(this.employee?.id).subscribe(response => {
          this.editEmployee = response
          this.form.patchValue({
            firstName: response.firstName,
            lastName: response.lastName,
            middleName: response.middleName,
            birthDate: response.birthDate,
            departmentId: response.departmentId,
            salary: response.salary
          })
        })
        this.departmentsService.getDepartments().subscribe(response => {
          this.departments = response.departments
          // this.form.patchValue({
          //   departmentId: this.departments.find(x => x.id == this.editEmployee.departmentId)?.id
          // });
        })
      }
    }
  
    submit(){
      Object.keys(this.form.controls).forEach(key => {
        const control = this.form.get(key);
        control?.markAsTouched();
      });
      if (this.form.valid) {
        this.editEmployee = {
          firstName: this.form.value.firstName || '',
          lastName: this.form.value.lastName || '',
          middleName: this.form.value.middleName || '',
          birthDate: this.form.value.birthDate || '',
          departmentId: this.form.value.departmentId || 0,
          salary: this.form.value.salary || 0,
        };
    
        this.employeesService.editEmployee(this.editEmployee).subscribe(() => {
          this.employeeEdited.emit();
          this.modalService.close();
        });
      }
    }
}
