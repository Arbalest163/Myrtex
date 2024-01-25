import { Routes } from '@angular/router';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { EmployeesPageComponent } from './pages/employees-page/employees-page.component';

export const routes: Routes = [
    { path: "about", component: AboutPageComponent },
    { path: "employees", component: EmployeesPageComponent },
    { path: "**", component: AboutPageComponent }
];
