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
    { field: 'id', headerName: 'ID', width: 70 },
    { field: 'type', headerName: 'Tipo', width: 130 },
    { 
      field: 'date',
      headerName: 'Data',
      width: 150,
      valueFormatter: ({ value }) => formatDate(value)
    },
    {
      field: 'value',  
      headerName: 'Valor',
      width: 130,
      valueFormatter: ({ value}) => formatCurrency(value)
    },
    { field: 'cpf', headerName: 'CPF', width: 130 },
    { field: 'card', headerName: 'Cartão', width: 130 },
    {
      field: 'time',
      headerName: 'Hora',
      width: 150,
    },
    { 
      field: 'storeOwner',
      headerName: 'Dono da Loja',
      width: 150,
    },
    {
      field: 'storeName',  
      headerName: 'Nome da Loja',
      width: 150,
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
        />
      )}
    </>
  );

}