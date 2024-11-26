import { QueryClient, QueryClientProvider } from "react-query";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Index } from "./pages/Index";
import { ReadNote } from "./pages/read-note";
import { PageNotFound } from "./pages/page-not-found";
import { Layout } from "./component/layout";

function App() {
  const queryClient = new QueryClient();
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Layout>
          <Routes>
            <Route index element={<Index />}></Route>
            <Route path="note/:noteId" element={<ReadNote />}></Route>
            <Route path="*" element={<PageNotFound />} />
          </Routes>
        </Layout>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
