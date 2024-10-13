import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {  ItemService } from '../../services/item.service';
import { Item } from '../../models/item.model';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss']
})
export class ItemListComponent {
  items: Item[] = [];
  newItem: Item = { id: 0, name: '', description: '' };
  currentItem: Item = { id: 0, name: '', description: '' };
  isEditing: boolean = false;

  constructor(private itemService: ItemService) {
    this.fetchItems();
  }

  fetchItems(): void {
    this.itemService.getItems().subscribe(items => {
      this.items = items;
    });
  }

  addItem(): void {
    if (this.newItem.name && this.newItem.description) {
      this.itemService.addItem(this.newItem).subscribe(item => {
        this.items.push(item);
        this.newItem = { id: 0, name: '', description: '' };
      });
    }
  }

  deleteItem(id: number): void {
    this.itemService.deleteItem(id).subscribe(() => {
      this.items = this.items.filter(item => item.id !== id);
    });
  }


  editItem(item: Item) {
    this.currentItem = { ...item }; // Clone the item to edit
    this.isEditing = true; // Set editing mode
  }

  updateItem() {
    const index = this.items.findIndex(i => i.id === this.currentItem.id);
    if (index !== -1) {
      this.items[index] = { ...this.currentItem }; // Update the item
    }
    this.cancelEdit(); // Reset the form
  }

  cancelEdit() {
    this.isEditing = false; 
    this.currentItem = { id: 0, name: '', description: '' }; // Clear current item
  }
}
