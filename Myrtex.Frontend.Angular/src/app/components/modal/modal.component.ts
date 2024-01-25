import { Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ModalService } from '../../services/modal.service';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss'
})
export class ModalComponent implements OnInit, OnDestroy{
    @Input() title: string = 'Заголовок'
    @Input() id: string
    isOpen = false
    private element: any
  
  constructor(public modalService: ModalService, private el: ElementRef){
    this.element = el.nativeElement
  }

  ngOnInit() {
    this.element.style.display = 'none'
    this.modalService.add(this)
    if(typeof document !== 'undefined')
      document.body.appendChild(this.element)
}

ngOnDestroy() {
    this.modalService.remove(this);
    this.element.remove();
}

open() {
    this.element.style.display = 'block'
    if(typeof document !== 'undefined')
      document.body.classList.add('app-modal-open')
    this.isOpen = true;
    
}

close() {
    this.element.style.display = 'none'
    if(typeof document !== 'undefined')
      document.body.classList.remove('app-modal-open')
    this.isOpen = false
  }
}
