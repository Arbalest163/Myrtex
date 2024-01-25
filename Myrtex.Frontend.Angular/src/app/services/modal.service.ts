import { Injectable } from '@angular/core';
import { ModalComponent } from '../components/modal/modal.component';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private modals: ModalComponent[] = []

    add(modal: ModalComponent) {
        if (!modal.id || this.modals.find(x => x.id === modal.id)) {
            throw new Error('modal must have a unique id attribute')
        }
        this.modals.push(modal)
    }

    remove(modal: ModalComponent) {
        this.modals = this.modals.filter(x => x.id !== modal.id)
    }

    open(id: string) {
        const modal = this.modals.find(x => x.id === id)
        if (!modal) {
            throw new Error(`modal '${id}' not found`)
        }
        modal.open()
    }

    close() {
        const modal = this.modals.find(x => x.isOpen);
        modal?.close()
    }

    isOpen(id: string) : boolean{
        const modal = this.modals.find(x => x.id === id)
        return modal?.isOpen ?? false
    }
}