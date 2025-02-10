export interface Booking {
  id: string;
  checkInDate: string;
  checkOutDate: string;
  status: string;
}

export interface AllBooking {
  id: string;
  checkInDate: string;
  checkOutDate: string;
  status: string;
  roomNumber: string;
  customerName: string;
}

// export const AllBookings: AllBooking[] = [];

// export const Bookings: Booking[] = [];
