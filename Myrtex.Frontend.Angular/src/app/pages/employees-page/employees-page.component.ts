import { Component, OnInit } from '@angular/core';
import { EmployeesService } from '../../services/employees.service';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { FilterField, FilterFieldRus, IEmployee, IGetEmployeesQuery } from '../../models/models';
import { IonicModule } from '@ionic/angular';
import { ModalComponent } from "../../components/modal/modal.component";
import { AddEmployeeComponent } from '../../components/add-employee/add-employee.component';
import { ModalService } from '../../services/modal.service';
import { DeleteEmployeeComponent } from "../../components/delete-employee/delete-employee.component";
import { EditEmployeeComponent } from "../../components/edit-employee/edit-employee.component";
import { LoadingComponent } from "../../components/loading/loading.component";

const CURRENT_PAGE = 'currentPage'

@Component({
    selector: 'app-employees-page',
    standalone: true,
    templateUrl: './employees-page.component.html',
    styleUrl: './employees-page.component.scss',
    imports: [CommonModule,
        NgxPaginationModule, FormsModule,
        IonicModule, ModalComponent,
        AddEmployeeComponent, DeleteEmployeeComponent, EditEmployeeComponent, LoadingComponent]
})
export class EmployeesPageComponent implements OnInit {
  loading = false
  employees: IEmployee[]
  totalPages: number
  totalItems: number
  query: IGetEmployeesQuery = {
    page: 1,
    pageSize: 5,
    filter: {
      orderInfo: {
        orderField: FilterField.Department,
        ascending: true,
      },
      searchInfo: {
        searchField: FilterField.Department,
        searchText: '',
      },
    },
  };
  filterFieldOptions = Object.values(FilterField)
  filterFileds = FilterField
  selectedEmployee: IEmployee

  orderFieldAscValue: Record<FilterField, boolean> = {
    [FilterField.Department]: true,
    [FilterField.LastName]: true,
    [FilterField.FirstName]: true,
    [FilterField.MiddleName]: true,
    [FilterField.BirthDate]: true,
    [FilterField.DateOfEmployment]: true,
    [FilterField.Salary]: true,
  };

  constructor(
    private employeesService: EmployeesService,
    public modalService: ModalService
    ) {}

  ngOnInit(): void {
    this.loading = true;

    if (typeof localStorage !== 'undefined') {
      const storedIndex = parseInt(localStorage.getItem(CURRENT_PAGE) || '1', 10)
      this.query.page = storedIndex
    }

    this.loadEmployees()
  }

  loadEmployees() {
    this.fixTypeSearchText()
    this.employeesService.getEmployees(this.query).subscribe((response) => {
      this.loading = false
      this.employees = response.items
      this.totalPages = response.totalPages
      this.totalItems = response.totalItems
      if(this.totalPages < this.query.page){
        this.pageChangeEvent(this.totalPages)
      }
    });
  }

  fixTypeSearchText(){
    this.query.filter.searchInfo.searchText = this.query.filter.searchInfo.searchText?.toString() ?? ''
  }

  applyFilter() {
    this.query.page = 1
    this.loadEmployees()
  }

  openEditEmployeeModal(employee: IEmployee) {
    this.selectedEmployee = employee
    this.modalService.open('edit-employee')
  }

  openAddEmployeeModal(){
    this.modalService.open('add-employee')
  }

  onSuccess(){
    this.loadEmployees()
  }

  openDeleteConfirmationModal(employee: IEmployee) {
    this.selectedEmployee = employee
    this.modalService.open('delete-employee')
  }

  pageChangeEvent(page: number) {
    this.query.page = page
    localStorage.setItem(CURRENT_PAGE, page.toString())
    this.loadEmployees()
  }

  pageSizeChangeEvent() {
    this.loadEmployees()
  }

  mapFilterFieldToRus(filterField: FilterField): string {
    const mapping: Record<FilterField, string> = {
      [FilterField.Department]: FilterFieldRus.Department,
      [FilterField.LastName]: FilterFieldRus.LastName,
      [FilterField.FirstName]: FilterFieldRus.FirstName,
      [FilterField.MiddleName]: FilterFieldRus.MiddleName,
      [FilterField.BirthDate]: FilterFieldRus.BirthDate,
      [FilterField.DateOfEmployment]: FilterFieldRus.DateOfEmployment,
      [FilterField.Salary]: FilterFieldRus.Salary,
    };
  
    return mapping[filterField] || '';
  }

  createEmptyArray(number: number){
    return new Array(number).fill(0);
  }

  sortColumn(sortField: FilterField){
    const ascending = this.orderFieldAscValue[sortField] = !this.orderFieldAscValue[sortField];
    this.query.filter.orderInfo.ascending = ascending
    this.query.filter.orderInfo.orderField = sortField
    this.loadEmployees()
  }
}
