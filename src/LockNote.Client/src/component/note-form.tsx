import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { faPlus, faMinus } from "@fortawesome/free-solid-svg-icons";
import { NoteDto } from "../types/NoteDto";

type Inputs = {
  message: string;
  numOfViews: number;
  password: string;
  passwordAgain: string;
};

type NoteFormProps = {
  mutate: (note: NoteDto) => void;
};

export default function NoteForm({ mutate }: NoteFormProps) {
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm<Inputs>();

  const password = watch("password");
  const numOfViews = watch("numOfViews");

  const onSubmit: SubmitHandler<Inputs> = () =>
    mutate({
      content: message,
      password: password,
      readBeforeDelete: numOfViews,
    } as NoteDto);

  const [isExpanded, setIsExpanded] = useState(false);
  const [message, setMessage] = useState<string>("");

  const toggleExpand = () => {
    setIsExpanded(!isExpanded);
  };

  return (
    /* "handleSubmit" will validate your inputs before invoking "onSubmit" */
    <form onSubmit={handleSubmit(onSubmit)}>
      <div className="flex flex-col gap-2">
        <label
          data-testid="message-label"
          htmlFor="message"
          className="mb-2 block text-sm font-medium text-gray-400"
        >
          Your message
        </label>
        <textarea
          data-testid="message"
          id="message"
          className="outline:ring-purple-700 block h-32 min-h-11 w-full rounded-lg border border-slate-700 bg-slate-950 bg-opacity-25 bg-gradient-to-tr from-purple-950/25 via-pink-950/10 to-red-950/10 p-2.5 text-sm text-white placeholder-gray-200 focus:border-purple-700 focus:ring-purple-700"
          placeholder="Your message..."
          {...register("message", {
            required: "The message field is required",
            maxLength: {
              value: 2000,
              message: "Max length is 2000 characters",
            },
            minLength: { value: 1, message: "Min length is 1 character" },
          })}
          onChange={(e) => setMessage(e.target.value)}
          value={message}
        />
        <p className="text-red-500" data-testid="message-error">
          {errors.message && errors.message.message}
        </p>
      </div>
      <div className="mt-4 space-y-4 rounded-lg border-[1px] border-slate-700 p-4">
        <button
          data-testid="expand-button"
          type="button"
          onClick={toggleExpand}
          className="w-full bg-slate-900 text-sm text-blue-500 hover:underline"
        >
          <FontAwesomeIcon
            icon={isExpanded ? faMinus : faPlus}
            className="mr-2"
          />
          {isExpanded ? "Hide advanced fields" : "Show advanced fields"}
        </button>

        {/* Expanding section */}
        {isExpanded && (
          <div className="flex flex-col gap-4">
            <div className="flex w-full flex-col gap-2">
              <label className="text-sm font-medium text-gray-400">
                Number of views
              </label>
              <select
                data-testid="num-of-reads"
                title="number of reads"
                className="w-full rounded-lg border border-slate-700 bg-slate-950 p-2 text-sm text-white placeholder-gray-400 focus:border-blue-500 focus:ring-blue-500"
                {...register("numOfViews", {
                  validate: (value) =>
                    (value > 0 && value <= 10) ||
                    "The number of views must be between 1 and 10",
                })}
                defaultValue={1}
              >
                {Array.from({ length: 10 }, (_, i) => (
                  <option key={i + 1} value={i + 1}>
                    {i + 1}
                  </option>
                ))}
              </select>
            </div>

            <hr className="border-dashed border-slate-700" />
            <div className="flex w-full flex-col gap-2">
              <label className="text-sm font-medium text-gray-400">
                Password
              </label>
              <input
                type="password"
                className="w-full rounded-lg border border-slate-700 bg-slate-950 p-2 text-sm text-white placeholder-gray-400 focus:border-blue-500 focus:ring-blue-500"
                placeholder="Enter a password"
                {...register("password", { required: "Password is required" })}
              />
              <label className="text-sm font-medium text-gray-400">
                Password again
              </label>
              <input
                type="password"
                className="w-full rounded-lg border border-slate-700 bg-slate-950 p-2 text-sm text-white placeholder-gray-400 focus:border-blue-500 focus:ring-blue-500"
                placeholder="Enter the same again password"
                {...register("passwordAgain", {
                  validate: (value) =>
                    value === password || "Passwords do not match",
                })}
              />
              <p className="text-red-500">
                {errors.passwordAgain && errors.passwordAgain.message}
              </p>
            </div>
          </div>
        )}
      </div>

      <button
        data-testid="submit-button"
        type="submit"
        className="mt-5 rounded-lg bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-600"
      >
        Create note
      </button>
    </form>
  );
}
