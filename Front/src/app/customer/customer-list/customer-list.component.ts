import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Customer } from '../models/customer.model';
import { CustomerService } from '../services/customerService';
import { MatDialog } from '@angular/material/dialog';
import { CustomerFormComponent } from '../customer-form/customer-form.component';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css'],
})
export class CustomerListComponent implements AfterViewInit {
  displayedColumns: string[] = [
    'firstName',
    'lastName',
    'dateOfBirth',
    'email',
    'phoneNumber',
    'bankAccountNumber',
    'actions',
  ];
  dataSource = new MatTableDataSource<Customer>();
  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  customers: Customer[] = [];
  constructor(
    private customerService: CustomerService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.getCustomers();
    this.dataSource.data = this.customers;
  }

  getCustomers() {
    this.customerService.getCustomers().subscribe((customers) => {
      this.dataSource.data = customers;
    });
  }

  deleteCustomer(customer: Customer) {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerService.deleteCustomer(customer.id).subscribe(() => {
        this.getCustomers();
      });
    }
  }

  openCustomerFormDialog(customer?: Customer) {
    const isAddMode = !customer;
    const dialogRef = this.dialog.open(CustomerFormComponent, {
    width: '1000px',
    data: {
    isAddMode,
    customer,
    },
    });

    dialogRef.afterClosed().subscribe((data) => {
      console.log('Dialog output:', data);
      this.getCustomers();
    });
  }
}
