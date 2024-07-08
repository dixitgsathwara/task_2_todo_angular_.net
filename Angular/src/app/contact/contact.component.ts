import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

interface Contact {
  id?: number;
  userid: string;
  name: string;
  email: string;
  phone: string;
}

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {
  newContact: Contact = { userid: '12345', name: '', email: '', phone: '' };
  editedContact: Contact = { userid: '12345', name: '', email: '', phone: '' };
  contacts: Contact[] = [];
  isEditing = false;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.getContacts();
  }

  addContact() {
    this.authService.createContact(this.newContact).subscribe((contact: Contact) => {
      this.contacts.push(contact);
      this.newContact = { userid: '12345', name: '', email: '', phone: '' };
    });
  }

  getContacts() {
    this.authService.getContacts().subscribe((contacts: Contact[]) => {
      this.contacts = contacts;
    });
  }

  editContact(contact: Contact) {
    if (contact) {
      this.editedContact = { ...contact }; // Make a copy of the contact
      this.isEditing = true;
    }
  }
  

  updateContact() {
    if (this.editedContact.id) { 
      const { id, ...updatedContact } = this.editedContact; 
      this.authService.updateContact(id, updatedContact).subscribe((updated: Contact) => {
        this.getContacts();
        this.isEditing = false;
        this.editedContact = { userid: '12345', name: '', email: '', phone: '' };
      });
    } else {
      console.error('Edited contact does not have a valid ID');
    }
  }

  cancelEdit() {
    this.isEditing = false;
    this.editedContact = { userid: '12345', name: '', email: '', phone: '' };
  }

  deleteContact(contactId: number | undefined) {
    if (contactId === undefined) {
      console.error('Contact ID is undefined');
      return;
    }
    this.authService.deleteContact(contactId).subscribe(() => {
      this.contacts = this.contacts.filter(c => c.id !== contactId);
    });
  }
}
