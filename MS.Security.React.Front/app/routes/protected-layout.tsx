import { useEffect, useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { isTokenValid } from '../auth';

export default function ProtectedLayout() {
  const [isReady, setIsReady] = useState(false);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    const valid = isTokenValid();
    setIsAuthenticated(valid);
    setIsReady(true);
  }, []);

  if (!isReady) return null;

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  return <Outlet />;
}