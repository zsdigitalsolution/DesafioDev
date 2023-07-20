import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  Button, 
  TextField, 
  CircularProgress 
} from '@mui/material';

import { useFormik } from 'formik';
import * as yup from 'yup';

import TransactionService from '../services/TransactionService';
import Transaction from '../models/Transaction';

const validationSchema = yup.object({
  value: yup
    .number('Informe um valor númerico')
    .required('O valor é obrigatório'),
  // outras validações   
});

export default function TransactionForm() {
  const navigate = useNavigate();
  
  const [isSubmitting, setIsSubmitting] = useState(false);

  const formik = useFormik({
    initialValues: {
      value: '',
       // outros valores iniciais
    },
    validationSchema,
    onSubmit: async (values) => {
      try {
        setIsSubmitting(true);

        const transaction = new Transaction(values);
        
        await TransactionService.createTransaction(transaction);
        
        navigate('/transactions');
      } catch (error) {
        console.error(error);
        // notificar erro para usuário
      } finally {
        setIsSubmitting(false);
      }
    }  
  });

  return (
    <form onSubmit={formik.handleSubmit}>
      <TextField 
        name="value"
        value={formik.values.value}
        onChange={formik.handleChange}
        error={formik.touched.value && Boolean(formik.errors.value)}
        helperText={formik.touched.value && formik.errors.value} 
      />
      // outros campos do formulário

      <Button 
        variant="contained" 
        color="primary" 
        type="submit"
        disabled={isSubmitting}
      >
        {isSubmitting ? <CircularProgress size={24} /> : 'Salvar'}
      </Button>
    </form>
  );
}