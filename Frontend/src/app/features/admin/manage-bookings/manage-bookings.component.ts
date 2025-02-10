import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { AdminService } from '../../../shared/services/admin.service';
import { AllBooking } from '../../../shared/interfaces/booking.interface';
import { AdminNavbarMenuComponent } from '../../../shared/components/admin-navbar-menu/admin-navbar-menu.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-manage-bookings',
  standalone: true,
  imports: [AdminNavbarMenuComponent, MatTableModule, MatSortModule, DatePipe],
  templateUrl: './manage-bookings.component.html',
  styleUrl: './manage-bookings.component.css',
})
export class ManageBookingsComponent {
  private _liveAnnouncer = inject(LiveAnnouncer);
  adminService = inject(AdminService);

  displayedColumns: string[] = [
    'id',
    'checkInDate',
    'checkOutDate',
    'status',
    'roomNumber',
    'customerName',
  ];
  dataSource = new MatTableDataSource<AllBooking>([]);

  @ViewChild(MatSort)
  sort: MatSort = new MatSort();

  ngOnInit() {
    this.adminService.bookingsHistory().subscribe(
      (data: AllBooking[]) => {
        console.log('Fetched Bookings:', data); // Debugging log
        this.dataSource.data = data;
      },
      (error) => {
        console.error('Error fetching bookings:', error);
      },
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
