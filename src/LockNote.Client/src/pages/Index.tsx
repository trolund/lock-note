import { useState } from "react";
import { useCreateNote } from "../api/client";
import { useQueryClient } from "react-query";
import { NoteDto } from "../types/NoteDto";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboard } from "@fortawesome/free-solid-svg-icons";

export const Index = () => {
  const queryClient = useQueryClient();
  const { mutate, data } = useCreateNote(queryClient);
  const [message, setMessage] = useState<string>("");

  const baseUrl = import.meta.env.VITE_FRONTEND_BASE_URL;

  // copy to clipboard button
  const copyToClipboard = async (data: NoteDto) => {
    if (!navigator.clipboard) {
      return;
    }

    await navigator.clipboard.writeText(`${baseUrl}/note/${data?.id}`);
  };

  if (data) {
    return (
      <div className="flex-col gap-4 max-w-2xl mx-auto min-h-8">
        <p>Your note has been created! You can access it at</p>
        <div className="flex flex-col">
          <Link
            to={`${baseUrl}/note/${data.id}`}
          >{`${baseUrl}/note/${data.id}`}</Link>
          <button
            type="button"
            className="px-4 py-2 mt-5 text-sm font-medium text-white bg-blue-500 rounded-lg hover:bg-blue-600"
            onClick={() => copyToClipboard(data)}
          >
            <FontAwesomeIcon icon={faClipboard} className="mr-2" />
            Copy to clipboard
          </button>
        </div>
      </div>
    );
  }
  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    mutate({ content: message } as NoteDto);
  };

  return (
    <div className="max-w-2xl mx-auto min-h-8">
      <form onSubmit={handleSubmit}>
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
          type="submit"
          className="mt-5 px-4 py-2 text-sm font-medium text-white bg-blue-500 rounded-lg hover:bg-blue-600"
        >
          Create note
        </button>
      </form>
      <p className="mt-5">Your note is automatically deleted after a month</p>
    </div>
  );
};
