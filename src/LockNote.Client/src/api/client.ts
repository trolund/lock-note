import { QueryClient, useMutation, useQuery } from "react-query";
import { NoteDto } from "../types/NoteDto";

const baseUrl = import.meta.env.VITE_BACKEND_BASE_URL;

const fetchNotes = async () => {
  const res = await fetch(baseUrl + "/api/Notes");
  return res.json();
};

// wrap the useQuery hook with a custom hook
export const useGetNotes = () => {
  return useQuery<NoteDto[], Error>("notes", fetchNotes);
};

// do the same for the getNoteById function
const fetchNoteById = async (id: string) => {
  const res = await fetch(`${baseUrl}/api/Notes/${id}`);
  return res.json();
};

export const useGetNoteById = (id: string) => {
  return useQuery<NoteDto, Error>(["note", id], () => fetchNoteById(id));
};

// do the same for the createNote function

const createNote = async (note: NoteDto) => {
  const res = await fetch(baseUrl + "/api/Notes", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(note),
  });
  return res.json();
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
