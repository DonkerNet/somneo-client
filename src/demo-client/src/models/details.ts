export default interface DetailsModel {
  device: {
    assignedName: string,
    typeNumber: string,
    serial: string
  },
  wifi: {
    ssid: string,
    protection: string,
    ipAddress: string,
    macAddress: string
  },
  firmware: {
    name: string,
    version: string
  },
  locale: {
    country: string,
    timezone: string
  }
}