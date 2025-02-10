import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { CustomerService } from '../../../shared/services/customer.service';
import { Booking } from '../../../shared/interfaces/booking.interface';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-booking-history',
  standalone: true,
  imports: [NavbarComponent, MatTableModule, MatSortModule, DatePipe],
  templateUrl: './booking-history.component.html',
  styleUrl: './booking-history.component.css',
})
export class BookingHistoryComponent {
  private _liveAnnouncer = inject(LiveAnnouncer);
  customerService = inject(CustomerService);

  displayedColumns: string[] = ['id', 'checkInDate', 'checkOutDate'];
  dataSource = new MatTableDataSource<Booking>([]);

  @ViewChild(MatSort)
  sort: MatSort = new MatSort();

  ngOnInit() {
    this.customerService.bookingHistoryCustomer().subscribe(
      (data: Booking[]) => {
        console.log('Fetched Bookings:', data); // Debugging log
        this.dataSource.data = data;
      },
      (error) => {},
    );
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  /** Announce the change in sort state for assistive technology. */
  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }
}
