export interface IPageRequest{
    page: number
    pageSize: number
}

export interface IPageResponse<TItem>{
    page: number
    pageSize: number
    totalPages: number
    totalItems: number
    items: TItem[]
}

export interface IUser{
    id: number
    firstName: string
    lastName: string
    middleName: string
    birthDate: string
  }
  
  export interface IEmployee extends IUser{
    department: string
    dateOfEmployment: string
    salary: string
  }

  export interface IDepartmentView{
    departments: IDepartment[]
  }
  
  export interface IDepartment{
    id: number
    name: string
  }
  
  export interface IEditEmployee{
    id?: number
    firstName: string
    lastName: string
    middleName?: string
    birthDate: string
    departmentId: number
    salary: number
  }
  
  /*
  public int Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string MidleName { get; set; }
  public DateTime BirthDate { get; set; }
  public int DepartmentId { get; set; }
  public decimal Salary { get; set; } */
  
  export enum FilterField {
      Department = 'Department',
      LastName = 'LastName',
      FirstName = 'FirstName',
      MiddleName = 'MiddleName',
      BirthDate = 'BirthDate',
      DateOfEmployment = 'DateOfEmployment',
      Salary = 'Salary',
    }
  
  export enum FilterFieldRus {
      Department = 'Департамент',
      LastName = 'Фамилия',
      FirstName = 'Имя',
      MiddleName = 'Отчество',
      BirthDate = 'Дата рождения',
      DateOfEmployment = 'Дата устройства на работу',
      Salary = 'Заработная плата',
    }
    
    export interface IOrderInfo {
      orderField: FilterField;
      ascending: boolean;
    }
    
    export interface ISearchInfo {
      searchField: FilterField;
      searchText: string;
    }
    
    export interface IFilterContext {
      orderInfo: IOrderInfo;
      searchInfo: ISearchInfo;
    }
  
    export interface IGetEmployeesQuery {
      page: number;
      pageSize: number;
      filter: IFilterContext;
    }