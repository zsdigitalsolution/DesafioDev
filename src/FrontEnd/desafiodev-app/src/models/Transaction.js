// Transaction.js

class Transaction {
  id;
  type;
  date;
  value;
  cpf;
  card; 
  time;
  storeOwner;
  storeName;

  constructor({
    id,
    type,
    date,
    value,
    cpf,
    card,
    time,
    storeOwner,
    storeName
  }) {
    this.id = id;
    this.type = type;
    this.date = date;
    this.value = value;
    this.cpf = cpf;
    this.card = card;
    this.time = time;
    this.storeOwner = storeOwner;
    this.storeName = storeName;
  }
}

export default Transaction;