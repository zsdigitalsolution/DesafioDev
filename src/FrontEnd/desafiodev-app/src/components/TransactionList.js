import React, { useState, useEffect } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { CircularProgress, Typography } from '@mui/material';

import TransactionService from '../services/TransactionService';
import { formatDate, formatCurrency } from '../utils/formatters';

export default function TransactionList() {
  const [transactions, setTransactions] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchTransactions();
  }, []);

    const typesConfig = {
        1: { 
            name: 'Débito',
            nature: 'Entrada',
            signal: '+'
        },
        2: {
            name: 'Boleto',
            nature: 'Saída', 
            signal: '-'
        },
        3: {
            name: 'Financiamento',
            nature: 'Saída',
            signal: '-'
        },
        4: {
            name: 'Crédito',
            nature: 'Entrada',
            signal: '+'
        },
        5: {
            name: 'Recebimento Empréstimo',
            nature: 'Entrada',
            signal: '+'
        },
        6: {
            name: 'Vendas',
            nature: 'Entrada',
            signal: '+'
        },
        7: {
            name: 'Recebimento TED',
            nature: 'Entrada', 
            signal: '+'
        },
        8: {
            name: 'Recebimento DOC',
            nature: 'Entrada',
            signal: '+'
        },
        9: {
            name: 'Aluguel',
            nature: 'Saída',
            signal: '-'
        }
}
    function getTypeInfo(type) {
        if(!typesConfig[type]) {
            return {}; // retorna objeto vazio
        }
        return typesConfig[type]; 
    }
  const fetchTransactions = async () => {
    try {
      setIsLoading(true);
      const data = await TransactionService.getTransactions();
      setTransactions(data);
    } catch (err) {
      setError(err);
    } finally {
      setIsLoading(false);
    }
  };

  const columns = [
      { field: 'id', headerName: 'ID', flex: 1 },
    
      {
          field: 'type',
          headerName: 'Tipo da transação',
          flex: 1,
          valueFormatter: ({ value }) => getTypeInfo(value).name
      },
    { 
      field: 'date',
      headerName: 'Data da ocorrência',
      flex: 1,
      valueFormatter: ({ value }) => formatDate(value)
    },
    {
      field: 'value',  
      headerName: 'Valor da movimentação',
      flex: 1,
      valueFormatter: ({ value }) => {
        return formatCurrency(value);
        }
    },
      {
          field: 'cpf',
          headerName: 'CPF do beneficiário',
          flex: 1
      },
      {
          field: 'card',
          headerName: 'Cartão utilizado na transação',
          flex: 1
      },
    {
      field: 'time',
      headerName: 'Hora',
      flex: 1
    },
    { 
      field: 'storeOwner',
      headerName: 'Nome do representante da loja',
      flex: 1
    },
    {
      field: 'storeName',  
      headerName: 'Nome da loja',
      flex: 1
    },
  ];

  return (
      <>
          
      <Typography variant="h5" height={80} align="center">Transações</Typography>

      {error && <Typography color="error" variant="h5" height={50}  align="center">{error.message}</Typography>}

      {isLoading ? <CircularProgress /> : (
        <DataGrid 
            rows={transactions}
            columns={columns}
            pageSize={5}
            rowsPerPageOptions={[5]}
            autoWidth
        />
      )}
    </>
  );

}