import { QueryClient, QueryClientProvider } from "react-query";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Index } from "./pages/Index";

function App() {
  const queryClient = new QueryClient();
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Index />}></Route>
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
