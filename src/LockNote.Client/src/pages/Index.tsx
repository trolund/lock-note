import { useCreateNote } from "../api/client";
import { useQueryClient } from "react-query";
import { NoteDto } from "../types/NoteDto";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClipboard } from "@fortawesome/free-solid-svg-icons";
import NoteForm from "../component/note-form";

export const Index = () => {
  const queryClient = useQueryClient();
  const { mutate, data } = useCreateNote(queryClient);

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
      <div className="mx-auto min-h-8 max-w-2xl flex-col gap-4">
        <p>Your note has been created! You can access it at</p>
        <div className="flex flex-col">
          <Link
            to={`${baseUrl}/note/${data.id}`}
          >{`${baseUrl}/note/${data.id}`}</Link>
          <button
            type="button"
            className="mt-5 rounded-lg bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-600"
            onClick={() => copyToClipboard(data)}
          >
            <FontAwesomeIcon icon={faClipboard} className="mr-2" />
            Copy to clipboard
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="mx-auto min-h-8 max-w-2xl">
      <NoteForm mutate={mutate} />
      <p className="mt-5">Your note is automatically deleted after a month</p>
    </div>
  );
};
