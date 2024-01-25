import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { IonicModule } from '@ionic/angular';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [CommonModule, IonicModule, RouterLink],
  providers: [],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss'
})
export class NavigationComponent implements OnInit  {
  activeItemIndex: number

  constructor(){}
  ngOnInit() {
    if(typeof localStorage !== 'undefined'){
      const storedIndex = parseInt(localStorage.getItem('activeItemIndex') || '0', 10)
      this.activeItemIndex = storedIndex
    }
    
  }

  setActiveIndex(index: number) {
    localStorage.setItem('activeItemIndex', index.toString())
    this.activeItemIndex = index
  }
}
