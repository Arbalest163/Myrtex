import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { NavigationComponent } from "./components/navigation/navigation.component";
import { EmployeesPageComponent } from './pages/employees-page/employees-page.component';
import { AboutPageComponent } from './pages/about-page/about-page.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { GlobalErrorComponent } from "./components/global-error/global-error.component";
import { ModalComponent } from "./components/modal/modal.component";

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [CommonModule, RouterOutlet, IonicModule, RouterLink, NavigationComponent, EmployeesPageComponent, AboutPageComponent, NgxPaginationModule, GlobalErrorComponent, ModalComponent]
})
export class AppComponent {
  title = "Myrtex.Frontend.Angular"
}
