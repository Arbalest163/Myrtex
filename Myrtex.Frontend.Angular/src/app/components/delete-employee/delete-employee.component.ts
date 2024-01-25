import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EmployeesService } from '../../services/employees.service';
import { ModalService } from '../../services/modal.service';
import { IEmployee } from '../../models/models';

@Component({
  selector: 'app-delete-employee',
  standalone: true,
  imports: [],
  templateUrl: './delete-employee.component.html',
  styleUrl: './delete-employee.component.scss'
})
export class DeleteEmployeeComponent {
  @Input() employee?: IEmployee
  @Output() employeeDeleted: EventEmitter<void> = new EventEmitter<void>();

  constructor(
    private employeesService: EmployeesService,
    public modalService: ModalService
  ){

  }

  deleteEmployee(){
    if(this.employee){
      this.employeesService.deleteEmployee(this.employee?.id).subscribe(() => {
        this.employeeDeleted.emit()
        this.modalService.close()
      })
    }
  }
}
