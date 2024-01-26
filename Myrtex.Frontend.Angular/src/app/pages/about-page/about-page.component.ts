import { Component } from '@angular/core';
import { LoadingComponent } from "../../components/loading/loading.component";

@Component({
    selector: 'app-about-page',
    standalone: true,
    templateUrl: './about-page.component.html',
    styleUrl: './about-page.component.scss',
    imports: [LoadingComponent]
})
export class AboutPageComponent {

}
