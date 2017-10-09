module PacketAssembler


type PacketGuy = {
  MessageId: byte
  PacketId: byte
  TotalPackets: byte // packetIds are 0 indexed so last packit will be totalpackets - 1
  Message: string
}