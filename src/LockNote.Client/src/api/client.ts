import { QueryClient, useMutation, useQuery } from "react-query";
import { NoteDto } from "../types/NoteDto";
import { isProduction } from "./node";

const baseUrl = isProduction()
  ? window.location.origin
  : import.meta.env.VITE_BACKEND_BASE_URL;

const fetchNotes = async () => {
  const res = await fetch(baseUrl + "/api/Notes");
  return res.json();
};

// wrap the useQuery hook with a custom hook
export const useGetNotes = () => {
  return useQuery<NoteDto[], Error>("notes", fetchNotes);
};

// create a custom hook to fetch notes by id and password
const fetchNoteByIdAndPassword = async (
  id: string,
  password?: string | null,
) => {
  const url = new URL(`${baseUrl}/api/Notes/${id}`);

  if (password) {
    url.searchParams.append("password", password);
  }

  const res = await fetch(url);
  return res.json();
};

export const useGetNoteById = (id: string, password?: string | null) => {
  return useQuery<NoteDto, Error>(["note", id], () =>
    fetchNoteByIdAndPassword(id, password),
  );
};

const createNote = async (note: NoteDto) => {
  const res = await fetch(`${baseUrl}/api/Notes`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(note),
  });
  return res.json() as Promise<NoteDto>;
};

// use the useMutation hook to create a note
export const useCreateNote = (queryClient: QueryClient) => {
  return useMutation(createNote, {
    onSuccess: () => {
      // invalidate the query to refetch the data
      queryClient.invalidateQueries("note");
    },
  });
};
