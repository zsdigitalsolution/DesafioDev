// TransactionService.js 

import axios from 'axios';
import Transaction from '../models/Transaction';

const API_URL = 'https://localhost:7201';

class TransactionService {
    
    async getTransactions() {
      let transactions = [];
    const response = await axios.get(`${API_URL}/transaction`);
    console.log(response);
    if (response.status !== 200) {
      throw new Error('Erro ao obter transações');
    }
        if (response.data) {
            transactions = response.data.map(t => {
                return new Transaction({ 
                    id: t.id,
                    type: t.type,
                    date: t.date,
                    value: t.value,
                    cpf: t.cpf,
                    card: t.card,
                    time: t.time, 
                    storeOwner: t.storeOwner,
                    storeName: t.storeName
                });
            });
        }
    else
        {
            throw new Error('Erro ao obter transações');
        }
    return transactions; 
  }  

  async getTransaction(id) {
    const response = await axios.get(`${API_URL}/transaction/${id}`);
    if(response.status !== 200) {
      throw new Error('Transação não encontrada');
    }
      console.log(response.data.data);
    return new Transaction(response.data.data);
  }

  async uploadFile(file) {
    const formData = new FormData();
    formData.append('file', file);

    const response = await axios.post(
      `${API_URL}/transaction`, 
      formData
    );
    if(response.status !== 201) {
      throw new Error('Erro ao enviar arquivo');
    }

    return response.data;
  }

}

export default new TransactionService();