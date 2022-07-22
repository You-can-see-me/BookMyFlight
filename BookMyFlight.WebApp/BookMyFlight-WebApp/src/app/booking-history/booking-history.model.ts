export interface BookingHistory {
    pnr: string,
    bookingId: number,
    airlineName: string,
    ticketCost: number,
    dateOfJourney: Date,
    bookingStatus: string
}