import { Component, OnInit, Inject, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Customer } from '../models/customer.model';
import { CustomerService } from '../services/customerService';
import { MatDialogRef } from '@angular/material/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css'],
})
export class CustomerFormComponent implements OnInit {
  customerFormm: FormGroup;
  @Input() isAddMode: boolean;
  @Input() customer: Customer = new Customer();

  constructor(
    private formBuilder: FormBuilder,
    private customerService: CustomerService,
    public dialogRef: MatDialogRef<CustomerFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.isAddMode = data.isAddMode;
    this.customer = data.customer;
    this.customerFormm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      bankAccountNumber: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.customerFormm.patchValue(this.customer);
  }

  onSubmit(): void {
    if (this.customerFormm.valid) {
      if (this.isAddMode) {
        this.customerService
          .addCustomer(this.customerFormm.value)
          .subscribe(() => {});
      } else {
        this.customerService
          .updateCustomer(this.customer.id, this.customerFormm.value)
          .subscribe(() => {});
      }
      this.dialogRef.close();
    } else {
    }
  }
}
