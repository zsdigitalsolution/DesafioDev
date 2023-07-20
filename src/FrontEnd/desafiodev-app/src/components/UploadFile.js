import React, { useState } from 'react';
import { 
  Button,
  CircularProgress  
} from '@mui/material';

import TransactionService from '../services/TransactionService';

export default function UploadFile() {
  const [file, setFile] = useState(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      setIsSubmitting(true);
      await TransactionService.uploadFile(file);
      
      setIsSubmitting(false);
      // notificar sucesso
    } catch (error) {
      setIsSubmitting(false);
      // notificar erro      
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input 
        type="file"
        onChange={(e) => setFile(e.target.files[0])}  
      />

      <Button
        variant="contained"
        color="primary"
        type="submit"
        disabled={isSubmitting}
      >
        {isSubmitting ? (
          <CircularProgress size={24} />
        ) : (
          'Enviar Arquivo'
        )}
      </Button>
    </form>
  );
}