import { useState } from "react";
import { useCreateNote } from "../api/client";
import { useQueryClient } from "react-query";
import { NoteDto } from "../types/NoteDto";
import { Link } from "react-router-dom";

export const Index = () => {
  const queryClient = useQueryClient();
  const { mutate, data } = useCreateNote(queryClient);
  const [message, setMessage] = useState<string>("");

  const baseUrl = import.meta.env.VITE_FRONTEND_BASE_URL;

  if (data) {
    return (
      <div className="max-w-2xl mx-auto min-h-8">
        <p>Your note has been created! You can access it at</p>
        <Link
          to={`${baseUrl}/note/${data.id}`}
        >{`${baseUrl}/note/${data.id}`}</Link>
      </div>
    );
  }

  return (
    <div className="max-w-2xl mx-auto min-h-8">
      <label
        htmlFor="message"
        className="block mb-2 text-sm font-medium text-gray-900 dark:text-gray-400"
      >
        Your message
      </label>
      <textarea
        id="message"
        className="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
        placeholder="Your message..."
        onChange={(e) => setMessage(e.target.value)}
      >
        {message}
      </textarea>
      <button
        onClick={() => mutate({ content: message } as NoteDto)}
        className="mt-5 px-4 py-2 text-sm font-medium text-white bg-blue-500 rounded-lg hover:bg-blue-600"
      >
        Create note
      </button>
      <p className="mt-5">Your note is automatically deleted after a month</p>
    </div>
  );
};
